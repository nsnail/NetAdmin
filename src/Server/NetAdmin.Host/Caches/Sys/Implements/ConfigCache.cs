using Furion.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Config;

namespace NetAdmin.Host.Caches.Sys.Implements;

/// <inheritdoc cref="NetAdmin.Host.Caches.Sys.IConfigCache" />
public class ConfigCache : CacheBase<IConfigService>, IScoped, IConfigCache
{
    private readonly TimeSpan _absoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
    private readonly TimeSpan _slidingExpiration               = TimeSpan.FromMinutes(5);

    /// <summary>
    ///     Initializes a new instance of the <see cref="ConfigCache" /> class.
    /// </summary>
    public ConfigCache(IMemoryCache cache, IConfigService service) //
        : base(cache, service) { }

    /// <inheritdoc />
    public Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryConfigRsp> Create(CreateConfigReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<int> Delete(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<QueryConfigRsp> GetLatestConfig()
    {
        return await Cache.GetOrCreateAsync( //
            $"{nameof(ConfigCache)}.{nameof(GetLatestConfig)}", async entry => {
                SetExpires(entry);
                return await Service.GetLatestConfig();
            });
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryConfigRsp>> PagedQuery(PagedQueryReq<QueryConfigReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryConfigRsp>> Query(QueryReq<QueryConfigReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryConfigRsp> Update(UpdateConfigReq req)
    {
        throw new NotImplementedException();
    }

    private void SetExpires(ICacheEntry entry)
    {
        entry.SetSlidingExpiration(_slidingExpiration);
        entry.AbsoluteExpirationRelativeToNow = _absoluteExpirationRelativeToNow;
    }
}