using NetAdmin.Domain.Dto.Sys.Captcha;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     人机验证服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class CaptchaController : ControllerBase<ICaptchaCache, ICaptchaService>, ICaptchaModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CaptchaController" /> class.
    /// </summary>
    public CaptchaController(ICaptchaCache cache) //
        : base(cache) { }

    /// <summary>
    ///     获取人机校验图
    /// </summary>
    [AllowAnonymous]
    public Task<GetCaptchaRsp> GetCaptchaImageAsync()
    {
        return Cache.GetCaptchaImageAsync();
    }

    /// <summary>
    ///     完成人机校验
    /// </summary>
    [AllowAnonymous]
    public Task<bool> VerifyCaptchaAsync(VerifyCaptchaReq req)
    {
        return Cache.VerifyCaptchaAsync(req);
    }
}