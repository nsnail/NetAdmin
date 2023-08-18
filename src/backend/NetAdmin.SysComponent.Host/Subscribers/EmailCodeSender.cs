using NetAdmin.Domain.Events.Sys;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

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
    [EventSubscribe(nameof(EmailCodeCreatedEvent))]
    public async Task SendEmailAsync(EventHandlerExecutingContext context)
    {
        if (context.Source is not EmailCodeCreatedEvent emailCodeCreatedEvent) {
            return;
        }

        await App.GetService<IEmailCache>().StoreEmailCodeInfoAsync(emailCodeCreatedEvent.Data);

        _logger.Info(emailCodeCreatedEvent);
    }
}