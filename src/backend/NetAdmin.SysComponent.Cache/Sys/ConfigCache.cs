using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Config;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <summary>
///     配置缓存
/// </summary>
public sealed class ConfigCache : DistributedCache<IConfigService>, IScoped, IConfigCache
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ConfigCache" /> class.
    /// </summary>
    public ConfigCache(IDistributedCache cache, IConfigService service) //
        : base(cache, service) { }

    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryConfigRsp> CreateAsync(CreateConfigReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryConfigReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryConfigRsp> GetAsync(QueryConfigReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryConfigRsp> GetLatestConfigAsync()
    {
        return Service.GetLatestConfigAsync();
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryConfigRsp>> PagedQueryAsync(PagedQueryReq<QueryConfigReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryConfigRsp>> QueryAsync(QueryReq<QueryConfigReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryConfigRsp> UpdateAsync(UpdateConfigReq req)
    {
        return Service.UpdateAsync(req);
    }
}