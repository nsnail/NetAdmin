using NetAdmin.Application.Services;

namespace NetAdmin.Cache;

/// <summary>
///     缓存基类
/// </summary>
public abstract class CacheBase<TCacheContainer, TService> : ICache<TCacheContainer, TService>
    where TService : IService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CacheBase{TCacheLoad, TService}" /> class.
    /// </summary>
    protected CacheBase(TCacheContainer cache, TService service)
    {
        Cache   = cache;
        Service = service;
    }

    /// <summary>
    ///     缓存对象
    /// </summary>
    public TCacheContainer Cache { get; }

    /// <summary>
    ///     关联的服务
    /// </summary>
    public TService Service { get; }
}