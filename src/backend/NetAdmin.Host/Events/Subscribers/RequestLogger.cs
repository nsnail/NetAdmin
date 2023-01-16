using Furion;
using Furion.EventBus;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.DataContract.Dto.Sys.User;
using NetAdmin.Host.Events.Sources;
using NSExt.Extensions;

namespace NetAdmin.Host.Events.Subscribers;

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

        // 截断过长的ResponseResult
        const int cutThreshold = 1000;
        if (operationEvent.Data.ResponseBody?.Length > cutThreshold) {
            operationEvent.Data.ResponseBody = $"{operationEvent.Data.ResponseBody.Sub(0, cutThreshold)}...";
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
        await logService.Create(operationEvent.Data);
    }
}