using Microsoft.Extensions.Caching.Memory;

namespace NetAdmin.Application.Caches;

/// <summary>
///     缓存接口
/// </summary>
public interface ICache
{
    /// <summary>
    ///     缓存对象
    /// </summary>
    IMemoryCache Cache { get; }
}