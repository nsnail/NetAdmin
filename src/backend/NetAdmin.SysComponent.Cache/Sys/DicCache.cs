using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Catalog;
using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <summary>
///     字典缓存
/// </summary>
public sealed class DicCache : DistributedCache<IDicService>, IScoped, IDicCache
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DicCache" /> class.
    /// </summary>
    public DicCache(IDistributedCache cache, IDicService service) //
        : base(cache, service) { }

    /// <inheritdoc />
    public Task<int> BulkDeleteCatalogAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteCatalogAsync(req);
    }

    /// <inheritdoc />
    public Task<int> BulkDeleteContentAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteContentAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDicCatalogRsp> CreateCatalogAsync(CreateDicCatalogReq req)
    {
        return Service.CreateCatalogAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDicContentRsp> CreateContentAsync(CreateDicContentReq req)
    {
        return Service.CreateContentAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteCatalogAsync(DelReq req)
    {
        return Service.DeleteCatalogAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteContentAsync(DelReq req)
    {
        return Service.DeleteContentAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDicCatalogRsp>> PagedQueryCatalogAsync(PagedQueryReq<QueryDicCatalogReq> req)
    {
        return Service.PagedQueryCatalogAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDicContentRsp>> PagedQueryContentAsync(PagedQueryReq<QueryDicContentReq> req)
    {
        return Service.PagedQueryContentAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryDicCatalogRsp>> QueryCatalogAsync(QueryReq<QueryDicCatalogReq> req)
    {
        return Service.QueryCatalogAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryDicContentRsp>> QueryContentAsync(QueryReq<QueryDicContentReq> req)
    {
        return Service.QueryContentAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDicCatalogRsp> UpdateCatalogAsync(UpdateDicCatalogReq req)
    {
        return Service.UpdateCatalogAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDicContentRsp> UpdateContentAsync(UpdateDicContentReq req)
    {
        return Service.UpdateContentAsync(req);
    }
}