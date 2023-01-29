using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;

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
}