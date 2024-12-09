using NetAdmin.Domain.Dto.Sys.Doc.Catalog;
using NetAdmin.Domain.Dto.Sys.Doc.Content;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDocService" />
public sealed class DocService(IDocCatalogService catalogService, IDocContentService contentService) //
    : ServiceBase<IDocService>, IDocService
{
    /// <inheritdoc />
    public Task<int> BulkDeleteCatalogAsync(BulkReq<DelReq> req)
    {
        req.ThrowIfInvalid();
        return catalogService.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<int> BulkDeleteContentAsync(BulkReq<DelReq> req)
    {
        req.ThrowIfInvalid();
        return contentService.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> ContentCountByAsync(QueryReq<QueryDocContentReq> req)
    {
        req.ThrowIfInvalid();
        return contentService.CountByAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocCatalogRsp> CreateCatalogAsync(CreateDocCatalogReq req)
    {
        req.ThrowIfInvalid();
        return catalogService.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocContentRsp> CreateContentAsync(CreateDocContentReq req)
    {
        req.ThrowIfInvalid();
        return contentService.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteCatalogAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return catalogService.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteContentAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return contentService.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocCatalogRsp> EditCatalogAsync(EditDocCatalogReq req)
    {
        req.ThrowIfInvalid();
        return catalogService.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocContentRsp> EditContentAsync(EditDocContentReq req)
    {
        req.ThrowIfInvalid();
        return contentService.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportContentAsync(QueryReq<QueryDocContentReq> req)
    {
        req.ThrowIfInvalid();
        return contentService.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocCatalogRsp> GetCatalogAsync(QueryDocCatalogReq req)
    {
        req.ThrowIfInvalid();
        return catalogService.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocContentRsp> GetContentAsync(QueryDocContentReq req)
    {
        req.ThrowIfInvalid();
        return contentService.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDocCatalogRsp>> PagedQueryCatalogAsync(PagedQueryReq<QueryDocCatalogReq> req)
    {
        req.ThrowIfInvalid();
        return catalogService.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDocContentRsp>> PagedQueryContentAsync(PagedQueryReq<QueryDocContentReq> req)
    {
        req.ThrowIfInvalid();
        return contentService.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryDocCatalogRsp>> QueryCatalogAsync(QueryReq<QueryDocCatalogReq> req)
    {
        req.ThrowIfInvalid();
        return catalogService.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryDocContentRsp>> QueryContentAsync(QueryReq<QueryDocContentReq> req)
    {
        req.ThrowIfInvalid();
        return contentService.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<int> SetEnabledAsync(SetDocContentEnabledReq req)
    {
        req.ThrowIfInvalid();
        return contentService.SetEnabledAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDocContentRsp> ViewContentAsync(QueryDocContentReq req)
    {
        req.ThrowIfInvalid();
        return contentService.ViewAsync(req);
    }
}