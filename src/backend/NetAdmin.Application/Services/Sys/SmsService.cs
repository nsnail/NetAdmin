using System.Globalization;
using Mapster;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Sys.Sms;
using NSExt.Extensions;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="ISmsService" />
public class SmsService : ServiceBase<ISmsService>, ISmsService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SmsService" /> class.
    /// </summary>
    public SmsService() { }

    /// <inheritdoc />
    public Task<SendSmsCodeRsp> SendSmsCode(SendSmsCodeReq req)
    {
        return Task.FromResult(req.Adapt<SendSmsCodeRsp>() with {
                                                                    CreateTime = DateTime.Now
                                                                  , Code = new[] { 0, 10000 }.Rand()
                                                                        .ToString(CultureInfo.InvariantCulture)
                                                                        .PadLeft(4, '0')
                                                                });
    }

    /// <inheritdoc />
    public Task<bool> VerifySmsCode(VerifySmsCodeReq req)
    {
        return Task.FromResult(true);
    }
}