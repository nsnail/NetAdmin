using NetAdmin.Domain.Dto.Sys.Sms;
using NetAdmin.Domain.Enums.Sys;
using NetAdmin.Domain.Events.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Subscribers;

/// <summary>
///     短信验证码发送器
/// </summary>
public sealed class SmsCodeSender : IEventSubscriber
{
    private readonly ILogger<SmsCodeSender> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SmsCodeSender" /> class.
    /// </summary>
    public SmsCodeSender(ILogger<SmsCodeSender> logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     发送短信
    /// </summary>
    [EventSubscribe(nameof(SmsCodeCreatedEvent))]
    public async Task SendSmsAsync(EventHandlerExecutingContext context)
    {
        if (context.Source is not SmsCodeCreatedEvent smsCodeCreatedEvent) {
            return;
        }

        // 发送...
        var smsService = App.GetService<ISmsService>();
        _ = await smsService.UpdateAsync(
            smsCodeCreatedEvent.Data.Adapt<UpdateSmsReq>() with { Status = SmsStatues.Sent });
        _logger.Info($"{nameof(ISmsService)}.{nameof(ISmsService.UpdateAsync)} {Ln.已完成}");
    }
}