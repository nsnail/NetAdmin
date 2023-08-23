using NetAdmin.Domain.Dto.Sys.VerifyCode;
using NetAdmin.Domain.Enums.Sys;
using NetAdmin.Domain.Events.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Subscribers;

/// <summary>
///     邮件验证码发送器
/// </summary>
public sealed class EmailCodeSender : IEventSubscriber
{
    private readonly ILogger<EmailCodeSender> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EmailCodeSender" /> class.
    /// </summary>
    public EmailCodeSender(ILogger<EmailCodeSender> logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     发送邮件
    /// </summary>
    [EventSubscribe(nameof(VerifyCodeCreatedEvent))]
    public async Task SendEmailAsync(EventHandlerExecutingContext context)
    {
        if (context.Source is not VerifyCodeCreatedEvent verifyCodeCreatedEvent ||
            verifyCodeCreatedEvent.Data.DeviceType != VerifyCodeDeviceTypes.Email) {
            return;
        }

        // 发送...
        var verifyCodeService = App.GetService<IVerifyCodeService>();
        _ = await verifyCodeService.UpdateAsync(
            verifyCodeCreatedEvent.Data.Adapt<UpdateVerifyCodeReq>() with { Status = VerifyCodeStatues.Sent });
        _logger.Info($"{nameof(IVerifyCodeService)}.{nameof(IVerifyCodeService.UpdateAsync)} {Ln.已完成}");
    }
}