using NetAdmin.Application.Services;

namespace NetAdmin.Cache;

/// <summary>
///     内存缓存
/// </summary>
public abstract class MemoryCache<TService>(IMemoryCache cache, TService service) : CacheBase<IMemoryCache, TService>(cache, service)
    where TService : IService;