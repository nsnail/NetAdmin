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

    /// <inheritdoc />
    public TCacheContainer Cache { get; }

    /// <inheritdoc />
    public TService Service { get; }
}