using NetAdmin.Domain.Dto.Sys.VerifyCode;
using NetAdmin.Domain.Enums.Sys;
using NetAdmin.Domain.Events.Sys;

namespace NetAdmin.SysComponent.Host.Subscribers;

/// <summary>
///     邮件验证码发送器
/// </summary>
public sealed class EmailCodeSender(ILogger<EmailCodeSender> logger) : IEventSubscriber
{
    /// <summary>
    ///     发送邮件
    /// </summary>
    [EventSubscribe]
    public async Task SendEmailAsync(VerifyCodeCreatedEvent @event) {
        if (@event.PayLoad.DeviceType != VerifyCodeDeviceTypes.Email) {
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