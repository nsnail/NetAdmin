using NetAdmin.Domain.Dto.Sys.VerifyCode;
using NetAdmin.Domain.Enums.Sys;
using NetAdmin.Domain.Events.Sys;

namespace NetAdmin.SysComponent.Host.Subscribers;

/// <summary>
///     短信验证码发送器
/// </summary>
public sealed class SmsCodeSender(ILogger<SmsCodeSender> logger) : IEventSubscriber
{
    /// <summary>
    ///     发送短信
    /// </summary>
    [EventSubscribe]
    public async Task SendSmsAsync(VerifyCodeCreatedEvent @event) {
        if (@event.PayLoad.DeviceType != VerifyCodeDeviceTypes.Mobile) {
            return;
        }

        // 发送...
        var verifyCodeService = App.GetService<IVerifyCodeService>();
        _ = await verifyCodeService
            .SetVerifyCodeStatusAsync(@event.PayLoad.Adapt<SetVerifyCodeStatusReq>() with { Status = VerifyCodeStatues.Sent })
            .ConfigureAwait(false);
        logger.Info($"{nameof(IVerifyCodeService)}.{nameof(IVerifyCodeService.SetVerifyCodeStatusAsync)} {Ln.已处理完毕}");
    }
}