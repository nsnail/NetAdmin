using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Catalog;
using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDicService" />
public sealed class DicService(IDicCatalogService catalogService, IDicContentService contentService) //
    : ServiceBase<IDicService>, IDicService
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
    public Task<QueryDicCatalogRsp> CreateCatalogAsync(CreateDicCatalogReq req)
    {
        req.ThrowIfInvalid();
        return catalogService.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDicContentRsp> CreateContentAsync(CreateDicContentReq req)
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
    public Task<int> EditCatalogAsync(EditDicCatalogReq req)
    {
        req.ThrowIfInvalid();
        return catalogService.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDicContentRsp> EditContentAsync(EditDicContentReq req)
    {
        req.ThrowIfInvalid();
        return contentService.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportContentAsync(QueryReq<QueryDicContentReq> req)
    {
        req.ThrowIfInvalid();
        return contentService.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDicCatalogRsp> GetCatalogAsync(QueryDicCatalogReq req)
    {
        req.ThrowIfInvalid();
        return catalogService.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDicContentRsp> GetContentAsync(QueryDicContentReq req)
    {
        req.ThrowIfInvalid();
        return contentService.GetAsync(req);
    }

    /// <inheritdoc />
    public async Task<string> GetDicValueAsync(GetDicValueReq req)
    {
        req.ThrowIfInvalid();
        var df = new DynamicFilterInfo {
                                           Filters = [
                                               new DynamicFilterInfo {
                                                                         Field    = nameof(QueryDicContentReq.CatalogId)
                                                                       , Operator = DynamicFilterOperators.Eq
                                                                       , Value    = req.CatalogId
                                                                     }
                                             , new DynamicFilterInfo {
                                                                         Field    = nameof(QueryDicContentReq.Key)
                                                                       , Operator = DynamicFilterOperators.Eq
                                                                       , Value    = req.Key
                                                                     }
                                           ]
                                       };
        var queryParam = new QueryReq<QueryDicContentReq> { Count = 1, DynamicFilter = df };
        return (await QueryContentAsync(queryParam).ConfigureAwait(false)).FirstOrDefault()?.Value;
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDicCatalogRsp>> PagedQueryCatalogAsync(PagedQueryReq<QueryDicCatalogReq> req)
    {
        req.ThrowIfInvalid();
        return catalogService.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDicContentRsp>> PagedQueryContentAsync(PagedQueryReq<QueryDicContentReq> req)
    {
        req.ThrowIfInvalid();
        return contentService.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryDicCatalogRsp>> QueryCatalogAsync(QueryReq<QueryDicCatalogReq> req)
    {
        req.ThrowIfInvalid();
        return catalogService.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryDicContentRsp>> QueryContentAsync(QueryReq<QueryDicContentReq> req)
    {
        req.ThrowIfInvalid();
        return contentService.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<int> SetEnabledAsync(SetDicContentEnabledReq req)
    {
        req.ThrowIfInvalid();
        return contentService.SetEnabledAsync(req);
    }
}