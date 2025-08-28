using NetAdmin.Application.Services;
using NetAdmin.Cache;

namespace NetAdmin.Host.Controllers;

/// <summary>
///     控制器基类
/// </summary>
public abstract class ControllerBase<TCache, TService>(TCache cache = default) : IDynamicApiController
    where TCache : ICache<IDistributedCache, TService>
    where TService : IService
{
    /// <summary>
    ///     关联的缓存
    /// </summary>
    protected TCache Cache => cache;
}