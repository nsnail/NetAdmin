using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Sys.Captcha;

namespace NetAdmin.Host.Caches.Sys;

/// <summary>
///     人机验证缓存接口
/// </summary>
public interface ICaptchaCache : ICache<ICaptchaService>, ICaptchaModule
{
    /// <summary>
    ///     删除缓存项
    /// </summary>
    void RemoveEntry(string id);

    /// <summary>
    ///     完成人机校验 ，并删除缓存项
    /// </summary>
    Task VerifyCaptchaAndRemoveAsync(VerifyCaptchaReq req);
}