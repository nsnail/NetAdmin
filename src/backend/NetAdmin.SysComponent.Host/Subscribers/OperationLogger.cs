using NetAdmin.Domain.Events.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Subscribers;

/// <summary>
///     操作日志记录
/// </summary>
public sealed class OperationLogger : IEventSubscriber
{
    /// <summary>
    ///     保存请求日志到数据库
    /// </summary>
    [EventSubscribe(nameof(RequestLogEvent))]
    public async Task OperationEventDbRecordAsync(EventHandlerExecutingContext context)
    {
        if (context.Source is not RequestLogEvent operationEvent) {
            return;
        }

        operationEvent.Data.TruncateStrings();
        _ = await App.GetService<IRequestLogService>().CreateAsync(operationEvent.Data).ConfigureAwait(false);
    }
}