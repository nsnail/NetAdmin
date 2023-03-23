using NetAdmin.Application.Services;

namespace NetAdmin.Host.Caches;

/// <summary>
///     缓存基类
/// </summary>
public class CacheBase<TService> : ICache<TService>
    where TService : IService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CacheBase{TService}" /> class.
    /// </summary>
    public CacheBase(IMemoryCache cache, TService service)
    {
        Cache   = cache;
        Service = service;
    }

    /// <inheritdoc />
    public IMemoryCache Cache { get; }

    /// <inheritdoc />
    public TService Service { get; }
}