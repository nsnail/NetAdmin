using Furion.FriendlyException;
using Microsoft.AspNetCore.Authorization;
using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Sys.Sms;
using NetAdmin.Host.Caches.Sys;

namespace NetAdmin.Host.Controllers.Sys;

/// <summary>
///     短信服务
/// </summary>
public class SmsController : ControllerBase<ISmsService>, ISmsModule
{
    private readonly ICaptchaCache _captchaCache;
    private readonly ISmsCache     _smsCache;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SmsController" /> class.
    /// </summary>
    public SmsController(ISmsService service, ICaptchaCache captchaCache, ISmsCache smsCache) //
        : base(service)
    {
        _captchaCache = captchaCache;
        _smsCache     = smsCache;
    }

    /// <summary>
    ///     发送短信验证码
    /// </summary>
    [AllowAnonymous]
    public async Task<SendSmsCodeRsp> SendSmsCode(SendSmsCodeReq req)
    {
        if (!await _captchaCache.VerifyCaptcha(req.VerifyCaptchaReq)) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation);
        }

        // 人机验证通过，删除人机验证缓存
        _captchaCache.RemoveEntry(req.VerifyCaptchaReq.Id);

        return await _smsCache.SendSmsCode(req);
    }

    /// <summary>
    ///     完成短信验证
    /// </summary>
    [AllowAnonymous]
    public async Task<bool> VerifySmsCode(VerifySmsCodeReq req)
    {
        return await Service.VerifySmsCode(req);
    }
}