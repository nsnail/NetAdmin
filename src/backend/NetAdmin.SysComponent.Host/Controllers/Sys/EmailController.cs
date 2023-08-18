using NetAdmin.Domain.Dto.Sys.Email;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     邮件服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class EmailController : ControllerBase<IEmailCache, IEmailService>, IEmailModule
{
    private readonly ICaptchaCache _captchaCache;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EmailController" /> class.
    /// </summary>
    public EmailController(IEmailCache cache, ICaptchaCache captchaCache) //
        : base(cache)
    {
        _captchaCache = captchaCache;
    }

    /// <summary>
    ///     发送邮箱验证码
    /// </summary>
    public async Task<SendEmailCodeRsp> SendEmailCodeAsync(SendEmailCodeReq req)
    {
        await _captchaCache.VerifyCaptchaAndRemoveAsync(req.VerifyCaptchaReq);
        return await Cache.SendEmailCodeAsync(req);
    }

    /// <summary>
    ///     完成邮箱验证
    /// </summary>
    public Task<bool> VerifyEmailCodeAsync(VerifyEmailCodeReq req)
    {
        return Cache.VerifyEmailCodeAsync(req);
    }
}