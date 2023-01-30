using Furion;
using Furion.EventBus;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Sys.RequestLog;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Events;
using NSExt.Extensions;

namespace NetAdmin.Host.Subscribers;

/// <summary>
///     请求日志记录
/// </summary>
public class RequestLogger : IEventSubscriber
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RequestLogger" /> class.
    /// </summary>
    public RequestLogger() { }

    /// <summary>
    ///     保存请求日志到数据库
    /// </summary>
    [EventSubscribe($"{nameof(OperationEvent)}")]
    public async Task OperationEventDbRecord(EventHandlerExecutingContext context)
    {
        if (context.Source is not OperationEvent operationEvent) {
            return;
        }

        CreateRequestLogReq logReq = null;

        // 登录日志 ，
        if (operationEvent.Data.ApiId.Equals("api/user/login", StringComparison.OrdinalIgnoreCase)) {
            try {
                var loginReq = operationEvent.Data.RequestBody.Object<PwdLoginReq>();
                logReq = operationEvent.Data with { ExtraData = loginReq.Account };
            }
            catch (Exception) {
                // ignored
            }
        }

        logReq ??= operationEvent.Data;
        var logService = App.GetRequiredService<IRequestLogService>();
        logReq.TruncateStrings();
        await logService.Create(logReq);
    }
}