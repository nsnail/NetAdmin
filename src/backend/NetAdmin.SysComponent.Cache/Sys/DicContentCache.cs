using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IDicContentCache" />
public sealed class DicContentCache(IDistributedCache cache, IDicContentService service) //
    : DistributedCache<IDicContentService>(cache, service), IScoped, IDicContentCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryDicContentReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDicContentRsp> CreateAsync(CreateDicContentReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryDicContentReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryDicContentReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDicContentRsp> GetAsync(QueryDicContentReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDicContentRsp>> PagedQueryAsync(PagedQueryReq<QueryDicContentReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryDicContentRsp>> QueryAsync(QueryReq<QueryDicContentReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<List<QueryDicContentRsp>> QueryByCatalogCodeAsync(string catalogCode)
    {
        #if !DEBUG
        return GetOrCreateAsync( //
            GetCacheKey(catalogCode), () => Service.QueryByCatalogCodeAsync(catalogCode), TimeSpan.FromSeconds(Numbers.SECS_CACHE_DIC_CATALOG_CODE));
        #else
        return Service.QueryByCatalogCodeAsync(catalogCode);
        #endif
    }

    /// <inheritdoc />
    public Task<int> SetEnabledAsync(SetDicContentEnabledReq req)
    {
        return Service.SetEnabledAsync(req);
    }
}