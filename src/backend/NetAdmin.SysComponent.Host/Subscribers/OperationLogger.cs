using NetAdmin.Domain.Events.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Subscribers;

/// <summary>
///     操作日志记录
/// </summary>
public sealed class OperationLogger : IEventSubscriber
{
    #if !DEBUG && DBTYPE_SQLSERVER
    private static readonly ConcurrentQueue<CreateRequestLogReq> _requestLogs = new();
    #endif

    /// <summary>
    ///     保存请求日志到数据库
    /// </summary>
    [EventSubscribe(nameof(RequestLogEvent))]
    public async Task OperationEventDbRecordAsync(EventHandlerExecutingContext context)
    {
        if (context.Source is not RequestLogEvent operationEvent) {
            return;
        }

        #if DEBUG || !DBTYPE_SQLSERVER
        _ = await App.GetService<IRequestLogService>().CreateAsync(operationEvent.Data).ConfigureAwait(false);
        #else
        if (_requestLogs.Count > Numbers.REQUEST_LOG_BUFF_SIZE) {
            await WriteToDbAsync().ConfigureAwait(false);
        }
        else {
            _requestLogs.Enqueue(operationEvent.Data);
        }
        #endif
    }

    #if !DEBUG && DBTYPE_SQLSERVER
    private static async Task WriteToDbAsync()
    {
        var inserts = new List<CreateRequestLogReq>(Numbers.REQUEST_LOG_BUFF_SIZE);

        // 批量入库
        for (var i = 0; i != Numbers.REQUEST_LOG_BUFF_SIZE; ++i) {
            if (!_requestLogs.TryDequeue(out var log)) {
                continue;
            }

            inserts.Add(log);
        }

        // 如果首尾日期不一致，要分别插入不同的日期分表
        if (inserts[0].CreatedTime.Date != inserts[^1].CreatedTime.Date) {
            foreach (var dayInserts in inserts.GroupBy(x => x.CreatedTime.Date)) {
                await App.GetService<IFreeSql>()
                         .Insert<Sys_RequestLog>(dayInserts.Select(x => x))
                         .ExecuteSqlBulkCopyAsync(tableName: $"{nameof(Sys_RequestLog)}_{dayInserts.Key:yyyyMMdd}")
                         .ConfigureAwait(false);
            }
        }
        else {
            await App.GetService<IFreeSql>()
                     .Insert<Sys_RequestLog>(inserts)
                     .ExecuteSqlBulkCopyAsync(tableName: $"{nameof(Sys_RequestLog)}_{inserts[0].CreatedTime:yyyyMMdd}")
                     .ConfigureAwait(false);
        }
    }
    #endif
}