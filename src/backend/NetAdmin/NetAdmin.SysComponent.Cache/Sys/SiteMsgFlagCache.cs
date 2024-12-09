using NetAdmin.Domain.Dto.Sys.SiteMsgFlag;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="ISiteMsgFlagCache" />
public sealed class SiteMsgFlagCache(IDistributedCache cache, ISiteMsgFlagService service)
    : DistributedCache<ISiteMsgFlagService>(cache, service), IScoped, ISiteMsgFlagCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QuerySiteMsgFlagReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QuerySiteMsgFlagReq> req)
    {
        return Service.CountByAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgFlagRsp> CreateAsync(CreateSiteMsgFlagReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgFlagRsp> EditAsync(EditSiteMsgFlagReq req)
    {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QuerySiteMsgFlagReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgFlagRsp> GetAsync(QuerySiteMsgFlagReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QuerySiteMsgFlagRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgFlagReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QuerySiteMsgFlagRsp>> QueryAsync(QueryReq<QuerySiteMsgFlagReq> req)
    {
        return Service.QueryAsync(req);
    }
}