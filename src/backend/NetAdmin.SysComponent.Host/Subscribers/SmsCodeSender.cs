using NetAdmin.SysComponent.Domain.Dto.Sys.VerifyCode;
using NetAdmin.SysComponent.Domain.Enums.Sys;
using NetAdmin.SysComponent.Domain.Events.Sys;

namespace NetAdmin.SysComponent.Host.Subscribers;

/// <summary>
///     短信验证码发送器
/// </summary>
public sealed class SmsCodeSender(ILogger<SmsCodeSender> logger) : IEventSubscriber
{
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
        _ = await verifyCodeService.SetVerifyCodeStatusAsync(
                                       verifyCodeCreatedEvent.Data.Adapt<SetVerifyCodeStatusReq>() with { Status = VerifyCodeStatues.Sent })
                                   .ConfigureAwait(false);
        logger.Info($"{nameof(IVerifyCodeService)}.{nameof(IVerifyCodeService.SetVerifyCodeStatusAsync)} {Ln.已处理完毕}");
    }
}