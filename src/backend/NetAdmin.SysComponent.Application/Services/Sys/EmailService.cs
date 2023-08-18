using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Sys.Email;
using NetAdmin.Domain.Events.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IEmailService" />
public sealed class EmailService : ServiceBase<IEmailService>, IEmailService
{
    private readonly IEventPublisher _eventPublisher;

    public EmailService(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }

    /// <inheritdoc />
    public async Task<SendEmailCodeRsp> SendEmailCodeAsync(SendEmailCodeReq req)
    {
        var ret = new SendEmailCodeRsp { Code = new[] { 0, 10000 }.Rand().ToInvString().PadLeft(4, '0') };

        // 发布邮件验证码创建事件
        var emailCodeCreatedEvent
            = new EmailCodeCreatedEvent(new EmailCodeStoreInfo { Code = ret.Code, EmailAddress = req.EmailAddress });
        await _eventPublisher.PublishAsync(emailCodeCreatedEvent);

        return ret;
    }

    /// <inheritdoc />
    public async Task<bool> VerifyEmailCodeAsync(VerifyEmailCodeReq req)
    {
        throw new NotImplementedException();
    }
}