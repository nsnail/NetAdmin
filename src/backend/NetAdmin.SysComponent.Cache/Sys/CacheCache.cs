using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Sys.Cache;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <summary>
///     缓存缓存
/// </summary>
public sealed class CacheCache : DistributedCache<ICacheService>, IScoped, ICacheCache
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CacheCache" /> class.
    /// </summary>
    public CacheCache(IDistributedCache cache, ICacheService service) //
        : base(cache, service) { }

    /// <inheritdoc />
    public Task<CacheStatisticsRsp> CacheStatisticsAsync()
    {
        return GetOrCreateAsync( //
            GetCacheKey(string.Empty), Service.CacheStatisticsAsync, TimeSpan.FromMinutes(1));
    }

    /// <inheritdoc />
    public void Clear()
    {
        Service.Clear();
    }
}