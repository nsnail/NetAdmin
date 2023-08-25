using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Catalog;
using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDicService" />
public sealed class DicService : ServiceBase<IDicService>, IDicService
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

    /// <summary>
    ///     批量删除字典目录
    /// </summary>
    public Task<int> BulkDeleteCatalogAsync(BulkReq<DelReq> req)
    {
        return _catalogService.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     批量删除字典内容
    /// </summary>
    public Task<int> BulkDeleteContentAsync(BulkReq<DelReq> req)
    {
        return _contentService.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     创建字典目录
    /// </summary>
    public Task<QueryDicCatalogRsp> CreateCatalogAsync(CreateDicCatalogReq req)
    {
        return _catalogService.CreateAsync(req);
    }

    /// <summary>
    ///     创建字典内容
    /// </summary>
    public Task<QueryDicContentRsp> CreateContentAsync(CreateDicContentReq req)
    {
        return _contentService.CreateAsync(req);
    }

    /// <summary>
    ///     删除字典目录
    /// </summary>
    public Task<int> DeleteCatalogAsync(DelReq req)
    {
        return _catalogService.DeleteAsync(req);
    }

    /// <summary>
    ///     删除字典内容
    /// </summary>
    public Task<int> DeleteContentAsync(DelReq req)
    {
        return _contentService.DeleteAsync(req);
    }

    /// <summary>
    ///     分页查询字典目录
    /// </summary>
    public Task<PagedQueryRsp<QueryDicCatalogRsp>> PagedQueryCatalogAsync(PagedQueryReq<QueryDicCatalogReq> req)
    {
        return _catalogService.PagedQueryAsync(req);
    }

    /// <summary>
    ///     分页查询字典内容
    /// </summary>
    public Task<PagedQueryRsp<QueryDicContentRsp>> PagedQueryContentAsync(PagedQueryReq<QueryDicContentReq> req)
    {
        return _contentService.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询字典目录
    /// </summary>
    public Task<IEnumerable<QueryDicCatalogRsp>> QueryCatalogAsync(QueryReq<QueryDicCatalogReq> req)
    {
        return _catalogService.QueryAsync(req);
    }

    /// <summary>
    ///     查询字典内容
    /// </summary>
    public Task<IEnumerable<QueryDicContentRsp>> QueryContentAsync(QueryReq<QueryDicContentReq> req)
    {
        return _contentService.QueryAsync(req);
    }

    /// <summary>
    ///     更新字典目录
    /// </summary>
    public Task<QueryDicCatalogRsp> UpdateCatalogAsync(UpdateDicCatalogReq req)
    {
        return _catalogService.UpdateAsync(req);
    }

    /// <summary>
    ///     更新字典内容
    /// </summary>
    public Task<QueryDicContentRsp> UpdateContentAsync(UpdateDicContentReq req)
    {
        return _contentService.UpdateAsync(req);
    }
}