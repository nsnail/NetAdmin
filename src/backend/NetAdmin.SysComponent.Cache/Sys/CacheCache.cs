using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Cache;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="ICacheCache" />
public sealed class CacheCache
    (IDistributedCache cache, ICacheService service) : DistributedCache<ICacheService>(cache, service), IScoped
                                                     , ICacheCache
{
    /// <inheritdoc />
    public Task<CacheStatisticsRsp> CacheStatisticsAsync()
    {
        return GetOrCreateAsync( //
            GetCacheKey(string.Empty), Service.CacheStatisticsAsync, TimeSpan.FromMinutes(1));
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<GetAllEntriesRsp>> GetAllEntriesAsync(PagedQueryReq<GetAllEntriesReq> req)
    {
        return Service.GetAllEntriesAsync(req);
    }
}