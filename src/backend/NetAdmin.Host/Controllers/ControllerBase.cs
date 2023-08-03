using NetAdmin.Application.Services;
using NetAdmin.Cache;

namespace NetAdmin.Host.Controllers;

/// <summary>
///     控制器基类
/// </summary>
public abstract class ControllerBase<TCache, TService> : IDynamicApiController
    where TCache : ICache<IDistributedCache, TService> //
    where TService : IService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ControllerBase{TCache, TService}" /> class.
    /// </summary>
    protected ControllerBase(TCache cache)
    {
        Cache = cache;
    }

    /// <summary>
    ///     关联的缓存
    /// </summary>
    protected TCache Cache { get; }
}