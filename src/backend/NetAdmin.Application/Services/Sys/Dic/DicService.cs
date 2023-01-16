using NetAdmin.Application.Services.Sys.Dependency.Dic;
using NetAdmin.DataContract.Dto.Sys.Dic.Catalog;
using NetAdmin.DataContract.Dto.Sys.Dic.Content;

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
    public ValueTask<int> BulkDeleteContent(BulkDelReq req)
    {
        return _contentService.BulkDelete(req);
    }

    /// <inheritdoc />
    public async ValueTask<QueryDicCatalogRsp> CreateCatalog(CreateDicCatalogReq req)
    {
        return await _catalogService.Create(req);
    }

    /// <inheritdoc />
    public async ValueTask<QueryDicContentRsp> CreateContent(CreateDicContentReq req)
    {
        return await _contentService.Create(req);
    }

    /// <inheritdoc />
    public async ValueTask<int> DeleteCatalog(DelReq req)
    {
        return await _catalogService.Delete(req);
    }

    /// <inheritdoc />
    public ValueTask<int> DeleteContent(DelReq req)
    {
        return _contentService.Delete(req);
    }

    /// <inheritdoc />
    public ValueTask<PagedQueryRsp<QueryDicCatalogRsp>> PagedQueryCatalog(PagedQueryReq<QueryDicCatalogReq> req)
    {
        return _catalogService.PagedQuery(req);
    }

    /// <inheritdoc />
    public ValueTask<PagedQueryRsp<QueryDicContentRsp>> PagedQueryContent(PagedQueryReq<QueryDicContentReq> req)
    {
        return _contentService.PagedQuery(req);
    }

    /// <inheritdoc />
    public ValueTask<List<QueryDicCatalogRsp>> QueryCatalog(QueryReq<QueryDicCatalogReq> req)
    {
        return _catalogService.Query(req);
    }

    /// <inheritdoc />
    public ValueTask<List<QueryDicContentRsp>> QueryContent(QueryReq<QueryDicContentReq> req)
    {
        return _contentService.Query(req);
    }

    /// <inheritdoc />
    public ValueTask<QueryDicCatalogRsp> UpdateCatalog(UpdateDicCatalogReq req)
    {
        return _catalogService.Update(req);
    }

    /// <inheritdoc />
    public ValueTask<QueryDicContentRsp> UpdateContent(UpdateDicContentReq req)
    {
        return _contentService.Update(req);
    }
}