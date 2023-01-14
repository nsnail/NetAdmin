using Microsoft.Extensions.Caching.Memory;

namespace NetAdmin.Application.Caches;

/// <summary>
///     缓存基类
/// </summary>
public class CacheBase : ICache
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CacheBase" /> class.
    /// </summary>
    public CacheBase(IMemoryCache memoryCache)
    {
        Cache = memoryCache;
    }

    /// <summary>
    ///     缓存对象
    /// </summary>
    public IMemoryCache Cache { get; }
}