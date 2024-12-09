using NetAdmin.Domain.Dto.Sys.Doc.Content;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IDocContentCache" />
public sealed class DocContentCache(IDistributedCache cache, IDocContentService service)
    : DistributedCache<IDocContentService>(cache, service), IScoped, IDocContentCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryDocContentReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryDocContentReq> req)
    {
        return Service.CountByAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocContentRsp> CreateAsync(CreateDocContentReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocContentRsp> EditAsync(EditDocContentReq req)
    {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryDocContentReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocContentRsp> GetAsync(QueryDocContentReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDocContentRsp>> PagedQueryAsync(PagedQueryReq<QueryDocContentReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryDocContentRsp>> QueryAsync(QueryReq<QueryDocContentReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<int> SetEnabledAsync(SetDocContentEnabledReq req)
    {
        return Service.SetEnabledAsync(req);
    }
}