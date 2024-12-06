using NetAdmin.Domain.Dto.Sys.Dic.Catalog;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IDicCatalogCache" />
public sealed class DicCatalogCache(IDistributedCache cache, IDicCatalogService service) //
    : DistributedCache<IDicCatalogService>(cache, service), IScoped, IDicCatalogCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryDicCatalogReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDicCatalogRsp> CreateAsync(CreateDicCatalogReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDicCatalogRsp> EditAsync(EditDicCatalogReq req)
    {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryDicCatalogReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDicCatalogRsp> GetAsync(QueryDicCatalogReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDicCatalogRsp>> PagedQueryAsync(PagedQueryReq<QueryDicCatalogReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryDicCatalogRsp>> QueryAsync(QueryReq<QueryDicCatalogReq> req)
    {
        return Service.QueryAsync(req);
    }
}