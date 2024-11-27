using NetAdmin.Domain.Dto.Sys.Doc.Catalog;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IDocCatalogCache" />
public sealed class DocCatalogCache(IDistributedCache cache, IDocCatalogService service)
    : DistributedCache<IDocCatalogService>(cache, service), IScoped, IDocCatalogCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryDocCatalogReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocCatalogRsp> CreateAsync(CreateDocCatalogReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryDocCatalogReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryDocCatalogReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocCatalogRsp> GetAsync(QueryDocCatalogReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDocCatalogRsp>> PagedQueryAsync(PagedQueryReq<QueryDocCatalogReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryDocCatalogRsp>> QueryAsync(QueryReq<QueryDocCatalogReq> req)
    {
        return Service.QueryAsync(req);
    }
}