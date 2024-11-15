using NetAdmin.Domain.Dto.Sys.Captcha;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     人机验证缓存
/// </summary>
public interface ICaptchaCache : ICache<IDistributedCache, ICaptchaService>, ICaptchaModule
{
    /// <summary>
    ///     完成人机校验 ，并删除缓存项
    /// </summary>
    Task VerifyCaptchaAndRemoveAsync(VerifyCaptchaReq req);
}