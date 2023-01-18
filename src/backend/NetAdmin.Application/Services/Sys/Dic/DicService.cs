using NetAdmin.Application.Services.Sys.Dependency.Dic;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Catalog;
using NetAdmin.Domain.Dto.Sys.Dic.Content;

namespace NetAdmin.Application.Services.Sys.Dic;

/// <inheritdoc cref="IDicService" />
public class DicService : ServiceBase<IDicService>, IDicService
{
    private readonly IDicCatalogService _catalogService;
    private readonly IDicContentService _contentService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DicService" /> class.
    /// </summary>
    public DicService(IDicCatalogService catalogService, IDicContentService contentService)
    {
        _catalogService = catalogService;
        _contentService = contentService;
    }

    /// <inheritdoc />
    public Task<int> BulkDeleteCatalog(BulkReq<DelReq> req)
    {
        return _catalogService.BulkDelete(req);
    }

    /// <inheritdoc />
    public Task<int> BulkDeleteContent(BulkReq<DelReq> req)
    {
        return _contentService.BulkDelete(req);
    }

    /// <inheritdoc />
    public async Task<QueryDicCatalogRsp> CreateCatalog(CreateDicCatalogReq req)
    {
        return await _catalogService.Create(req);
    }

    /// <inheritdoc />
    public async Task<QueryDicContentRsp> CreateContent(CreateDicContentReq req)
    {
        return await _contentService.Create(req);
    }

    /// <inheritdoc />
    public async Task<int> DeleteCatalog(DelReq req)
    {
        return await _catalogService.Delete(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteContent(DelReq req)
    {
        return _contentService.Delete(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDicCatalogRsp>> PagedQueryCatalog(PagedQueryReq<QueryDicCatalogReq> req)
    {
        return _catalogService.PagedQuery(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDicContentRsp>> PagedQueryContent(PagedQueryReq<QueryDicContentReq> req)
    {
        return _contentService.PagedQuery(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryDicCatalogRsp>> QueryCatalog(QueryReq<QueryDicCatalogReq> req)
    {
        return _catalogService.Query(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryDicContentRsp>> QueryContent(QueryReq<QueryDicContentReq> req)
    {
        return _contentService.Query(req);
    }

    /// <inheritdoc />
    public Task<QueryDicCatalogRsp> UpdateCatalog(UpdateDicCatalogReq req)
    {
        return _catalogService.Update(req);
    }

    /// <inheritdoc />
    public Task<QueryDicContentRsp> UpdateContent(UpdateDicContentReq req)
    {
        return _contentService.Update(req);
    }
}