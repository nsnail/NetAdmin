using NetAdmin.Domain.Dto.Sys.Doc.Catalog;
using NetAdmin.Domain.Dto.Sys.Doc.Content;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IDocCache" />
public sealed class DocCache(IDistributedCache cache, IDocService service) : DistributedCache<IDocService>(cache, service), IScoped, IDocCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteCatalogAsync(BulkReq<DelReq> req) {
        return Service.BulkDeleteCatalogAsync(req);
    }

    /// <inheritdoc />
    public Task<int> BulkDeleteContentAsync(BulkReq<DelReq> req) {
        return Service.BulkDeleteContentAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> ContentCountByAsync(QueryReq<QueryDocContentReq> req) {
        return Service.ContentCountByAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocCatalogRsp> CreateCatalogAsync(CreateDocCatalogReq req) {
        return Service.CreateCatalogAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocContentRsp> CreateContentAsync(CreateDocContentReq req) {
        return Service.CreateContentAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteCatalogAsync(DelReq req) {
        return Service.DeleteCatalogAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteContentAsync(DelReq req) {
        return Service.DeleteContentAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocCatalogRsp> EditCatalogAsync(EditDocCatalogReq req) {
        return Service.EditCatalogAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocContentRsp> EditContentAsync(EditDocContentReq req) {
        return Service.EditContentAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportContentAsync(QueryReq<QueryDocContentReq> req) {
        return Service.ExportContentAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocCatalogRsp> GetCatalogAsync(QueryDocCatalogReq req) {
        return Service.GetCatalogAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocContentRsp> GetContentAsync(QueryDocContentReq req) {
        return Service.GetContentAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDocCatalogRsp>> PagedQueryCatalogAsync(PagedQueryReq<QueryDocCatalogReq> req) {
        return Service.PagedQueryCatalogAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDocContentRsp>> PagedQueryContentAsync(PagedQueryReq<QueryDocContentReq> req) {
        return Service.PagedQueryContentAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryDocCatalogRsp>> QueryCatalogAsync(QueryReq<QueryDocCatalogReq> req) {
        return Service.QueryCatalogAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryDocContentRsp>> QueryContentAsync(QueryReq<QueryDocContentReq> req) {
        return Service.QueryContentAsync(req);
    }

    /// <inheritdoc />
    public Task<int> SetEnabledAsync(SetDocContentEnabledReq req) {
        return Service.SetEnabledAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocContentRsp> ViewContentAsync(QueryDocContentReq req) {
        return Service.ViewContentAsync(req);
    }
}