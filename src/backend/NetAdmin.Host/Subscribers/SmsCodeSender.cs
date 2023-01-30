using Furion;
using Furion.EventBus;
using Mapster;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Sms;
using NetAdmin.Domain.Events;
using NSExt.Extensions;

namespace NetAdmin.Host.Subscribers;

/// <summary>
///     短信验证码发送器
/// </summary>
public class SmsCodeSender : IEventSubscriber
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
    public async Task SyncApi(EventHandlerExecutingContext context)
    {
        if (context.Source is not SmsCodeCreatedEvent smsCodeCreatedEvent) {
            return;
        }

        // 发送...
        Console.WriteLine(1);

        var smsService = App.GetRequiredService<ISmsService>();
        await smsService.Update(smsCodeCreatedEvent.Data.Adapt<UpdateSmsReq>() with { Status = TbSysSms.Statues.Sent });
        _logger.Info($"{nameof(ISmsService)}.{nameof(ISmsService.Update)} {Ln.Completed}");
    }
}