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
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryConfigRsp> CreateAsync(CreateConfigReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryConfigRsp> GetLatestConfigAsync()
    {
        return Cache.GetOrCreateAsync( //
            $"{nameof(ConfigCache)}.{nameof(GetLatestConfigAsync)}", async entry => {
                SetExpires(entry);
                return await Service.GetLatestConfigAsync();
            });
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryConfigRsp>> PagedQueryAsync(PagedQueryReq<QueryConfigReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryConfigRsp>> QueryAsync(QueryReq<QueryConfigReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryConfigRsp> UpdateAsync(UpdateConfigReq req)
    {
        throw new NotImplementedException();
    }

    private void SetExpires(ICacheEntry entry)
    {
        _                                     = entry.SetSlidingExpiration(_slidingExpiration);
        entry.AbsoluteExpirationRelativeToNow = _absoluteExpirationRelativeToNow;
    }
}