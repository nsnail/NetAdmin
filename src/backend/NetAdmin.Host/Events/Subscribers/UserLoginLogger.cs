using Furion;
using Furion.EventBus;
using NetAdmin.Application.Service.Sys;
using NetAdmin.Host.Events.Sources;

namespace NetAdmin.Host.Events.Subscribers;

/// <summary>
///     登录日志记录
/// </summary>
public class UserLoginLogger : IEventSubscriber
{
    private readonly ILogger<UserLoginLogger> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="UserLoginLogger" /> class.
    /// </summary>
    public UserLoginLogger(ILogger<UserLoginLogger> logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     保存登录日志到数据库
    /// </summary>
    [EventSubscribe($"{nameof(UserLoginEvent)}")]
    public async Task UserLoginEventDbRecord(EventHandlerExecutingContext context)
    {
        if (context.Source is not UserLoginEvent operationEvent) {
            return;
        }

        var logService = App.GetRequiredService<ILogService>();
        await logService.CreateLoginLog(operationEvent.Data);
    }
}