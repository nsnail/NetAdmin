using Furion;
using Furion.EventBus;
using Mapster;
using NetAdmin.Application.Services.Sys;
using NetAdmin.DataContract.Dto.Sys.Log;
using NetAdmin.Host.Events.Sources;
using NSExt.Extensions;

namespace NetAdmin.Host.Events.Subscribers;

/// <summary>
///     请求日志记录
/// </summary>
public class RequestLogger : IEventSubscriber
{
    private readonly IEventPublisher        _eventPublisher;
    private readonly ILogger<RequestLogger> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RequestLogger" /> class.
    /// </summary>
    public RequestLogger(ILogger<RequestLogger> logger, IEventPublisher eventPublisher)
    {
        _logger         = logger;
        _eventPublisher = eventPublisher;
    }

    /// <summary>
    ///     保存操作日志到数据库
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

        var logService = App.GetRequiredService<ILogService>();
        await logService.CreateOperationLog(operationEvent.Data);

        // 发布登录事件
        if (operationEvent.Data.ApiId.Equals("api/user/login", StringComparison.OrdinalIgnoreCase)) {
            await _eventPublisher.PublishAsync(new UserLoginEvent(operationEvent.Data.Adapt<CreateLoginLogReq>()));
        }
    }
}