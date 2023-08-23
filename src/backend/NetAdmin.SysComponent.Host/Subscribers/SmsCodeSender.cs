using NetAdmin.Domain.Dto.Sys.VerifyCode;
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
    [EventSubscribe(nameof(VerifyCodeCreatedEvent))]
    public async Task SendSmsAsync(EventHandlerExecutingContext context)
    {
        if (context.Source is not VerifyCodeCreatedEvent verifyCodeCreatedEvent ||
            verifyCodeCreatedEvent.Data.DeviceType != VerifyCodeDeviceTypes.Mobile) {
            return;
        }

        // 发送...
        var verifyCodeService = App.GetService<IVerifyCodeService>();
        _ = await verifyCodeService.UpdateAsync(
            verifyCodeCreatedEvent.Data.Adapt<UpdateVerifyCodeReq>() with { Status = VerifyCodeStatues.Sent });
        _logger.Info($"{nameof(IVerifyCodeService)}.{nameof(IVerifyCodeService.UpdateAsync)} {Ln.已完成}");
    }
}