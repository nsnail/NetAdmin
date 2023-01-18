using Furion;
using Furion.EventBus;
using NetAdmin.Application.Services.Sys.Dependency;
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

        // 登录日志 ，
        if (operationEvent.Data.ApiId.Equals("api/user/login", StringComparison.OrdinalIgnoreCase)) {
            try {
                var loginReq = operationEvent.Data.RequestBody.Object<LoginReq>();
                operationEvent.Data.ExtraData = loginReq.UserName;
            }
            catch (Exception) {
                // ignored
            }
        }

        var logService = App.GetRequiredService<IRequestLogService>();
        operationEvent.Data.TruncateStrings();
        await logService.Create(operationEvent.Data);
    }
}