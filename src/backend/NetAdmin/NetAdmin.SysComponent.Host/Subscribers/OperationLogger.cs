#if !DEBUG && DBTYPE_SQLSERVER
using System.Collections.Concurrent;
using FreeSql.Provider.SqlServer;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.LoginLog;
using NetAdmin.Domain.Dto.Sys.RequestLog;
using NetAdmin.Domain.Events.Sys;

namespace NetAdmin.SysComponent.Host.Subscribers;

/// <summary>
///     操作日志记录
/// </summary>
public sealed class OperationLogger : IEventSubscriber
{
    private static readonly ConcurrentQueue<CreateRequestLogReq> _requestLogs = new();

    /// <summary>
    ///     保存请求日志到数据库
    /// </summary>
    [EventSubscribe]
    #pragma warning disable CA1822
    public async Task OperationEventDbRecordAsync(RequestLogEvent @event)
        #pragma warning restore CA1822
    {
        if (_requestLogs.Count > Numbers.REQUEST_LOG_BUFF_SIZE) {
            await WriteToDbAsync().ConfigureAwait(false);
        }
        else {
            _requestLogs.Enqueue(@event.PayLoad);
        }
    }

    private static async Task WriteToDbAsync()
    {
        var inserts = new List<CreateRequestLogReq>(Numbers.REQUEST_LOG_BUFF_SIZE);

        // 批量入库
        for (var i = 0; i != Numbers.REQUEST_LOG_BUFF_SIZE; ++i) {
            if (!_requestLogs.TryDequeue(out var log)) {
                continue;
            }

            inserts.Add(log);

            // 插入登录日志
            if (log.ApiPathCrc32 == Chars.FLG_PATH_API_SYS_USER_LOGIN_BY_PWD.Crc32()) {
                _ = await S<ILoginLogCache>().CreateAsync(log.Adapt<CreateLoginLogReq>()).ConfigureAwait(false);
            }
        }

        // 如果首尾日期不一致，要分别插入不同的日期分表
        if (inserts[0].CreatedTime.Date != inserts[^1].CreatedTime.Date) {
            foreach (var dayInserts in inserts.GroupBy(x => x.CreatedTime.Date)) {
                await S<IFreeSql>()
                         .Insert<Sys_RequestLog>(dayInserts.Select(x => x))
                         .ExecuteSqlBulkCopyAsync(tableName: $"{nameof(Sys_RequestLog)}_{dayInserts.Key:yyyyMMdd}")
                         .ConfigureAwait(false);
            }
        }
        else {
            await S<IFreeSql>()
                     .Insert<Sys_RequestLog>(inserts)
                     .ExecuteSqlBulkCopyAsync(tableName: $"{nameof(Sys_RequestLog)}_{inserts[0].CreatedTime:yyyyMMdd}")
                     .ConfigureAwait(false);
        }
    }
}
#else
using NetAdmin.Domain.Events.Sys;

namespace NetAdmin.SysComponent.Host.Subscribers;

/// <summary>
///     操作日志记录
/// </summary>
public sealed class OperationLogger : IEventSubscriber
{
    /// <summary>
    ///     保存请求日志到数据库
    /// </summary>
    [EventSubscribe]
    #pragma warning disable CA1822
    public async Task OperationEventDbRecordAsync(RequestLogEvent @event)
        #pragma warning restore CA1822
    {
        _ = await App.GetService<IRequestLogCache>().CreateAsync(@event.PayLoad).ConfigureAwait(false);
    }
}
#endif