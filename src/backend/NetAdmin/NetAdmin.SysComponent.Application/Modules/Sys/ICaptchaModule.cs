using NetAdmin.Domain.Dto.Sys.Captcha;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     人机验证模块
/// </summary>
public interface ICaptchaModule
{
    /// <summary>
    ///     获取人机校验图
    /// </summary>
    Task<GetCaptchaRsp> GetCaptchaImageAsync();

    /// <summary>
    ///     完成人机校验
    /// </summary>
    Task<bool> VerifyCaptchaAsync(VerifyCaptchaReq req);
}