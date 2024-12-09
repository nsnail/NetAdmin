using NetAdmin.Domain.Dto.Sys.Config;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IConfigCache" />
public sealed class ConfigCache(IDistributedCache cache, IConfigService service) //
    : DistributedCache<IConfigService>(cache, service), IScoped, IConfigCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryConfigReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryConfigReq> req)
    {
        return Service.CountByAsync(req);
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
    public Task<QueryConfigRsp> EditAsync(EditConfigReq req)
    {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryConfigReq> req)
    {
        return Service.ExportAsync(req);
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
    public Task<int> SetEnabledAsync(SetConfigEnabledReq req)
    {
        return Service.SetEnabledAsync(req);
    }
}