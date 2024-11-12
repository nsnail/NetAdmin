using NetAdmin.Application.Services;

namespace NetAdmin.Cache;

/// <summary>
///     缓存基类
/// </summary>
public abstract class CacheBase<TCacheContainer, TService>(TCacheContainer cache, TService service) : ICache<TCacheContainer, TService>
    where TService : IService
{
    /// <inheritdoc />
    public TCacheContainer Cache => cache;

    /// <inheritdoc />
    public TService Service => service;
}