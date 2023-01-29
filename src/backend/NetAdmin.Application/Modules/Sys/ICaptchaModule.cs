using NetAdmin.Domain.Dto.Sys.Captcha;

namespace NetAdmin.Application.Modules.Sys;

/// <summary>
///     人机验证模块
/// </summary>
public interface ICaptchaModule
{
    /// <summary>
    ///     获取人机校验图
    /// </summary>
    Task<GetCaptchaRsp> GetCaptchaImage();

    /// <summary>
    ///     完成人机校验
    /// </summary>
    Task<bool> VerifyCaptcha(VerifyCaptchaReq req);
}