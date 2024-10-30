using NetAdmin.Domain.Dto.Sys.Captcha;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     人机验证服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class CaptchaController(ICaptchaCache cache) : ControllerBase<ICaptchaCache, ICaptchaService>(cache), ICaptchaModule
{
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