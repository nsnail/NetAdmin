using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Sys.Cache;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="ICacheCache" />
public sealed class CacheCache(IDistributedCache cache, ICacheService service) //
    : DistributedCache<ICacheService>(cache, service), IScoped, ICacheCache
{
    /// <inheritdoc />
    public Task<CacheStatisticsRsp> CacheStatisticsAsync()
    {
        #if !DEBUG
        return GetOrCreateAsync( //
            GetCacheKey(string.Empty), Service.CacheStatisticsAsync, TimeSpan.FromSeconds(Numbers.SECS_CACHE_DEFAULT));
        #else
        return Service.CacheStatisticsAsync();
        #endif
    }

    /// <inheritdoc />
    public Task<IEnumerable<GetEntryRsp>> GetAllEntriesAsync(GetAllEntriesReq req)
    {
        return Service.GetAllEntriesAsync(req);
    }

    /// <inheritdoc />
    public Task<GetEntryRsp> GetEntryAsync(GetEntriesReq req)
    {
        return Service.GetEntryAsync(req);
    }
}