using NetAdmin.SysComponent.Domain.Dto.Sys.Dic.Catalog;
using NetAdmin.SysComponent.Domain.Dto.Sys.Dic.Content;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     字典服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class DicController(IDicCache cache) : ControllerBase<IDicCache, IDicService>(cache), IDicModule
{
    /// <summary>
    ///     批量删除字典目录
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteCatalogAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteCatalogAsync(req);
    }

    /// <summary>
    ///     批量删除字典内容
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteContentAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteContentAsync(req);
    }

    /// <summary>
    ///     创建字典目录
    /// </summary>
    [Transaction]
    public Task<QueryDicCatalogRsp> CreateCatalogAsync(CreateDicCatalogReq req)
    {
        return Cache.CreateCatalogAsync(req);
    }

    /// <summary>
    ///     创建字典内容
    /// </summary>
    [Transaction]
    public Task<QueryDicContentRsp> CreateContentAsync(CreateDicContentReq req)
    {
        return Cache.CreateContentAsync(req);
    }

    /// <summary>
    ///     删除字典目录
    /// </summary>
    [Transaction]
    public Task<int> DeleteCatalogAsync(DelReq req)
    {
        return Cache.DeleteCatalogAsync(req);
    }

    /// <summary>
    ///     删除字典内容
    /// </summary>
    [Transaction]
    public Task<int> DeleteContentAsync(DelReq req)
    {
        return Cache.DeleteContentAsync(req);
    }

    /// <summary>
    ///     编辑字典目录
    /// </summary>
    [Transaction]
    public Task<int> EditCatalogAsync(EditDicCatalogReq req)
    {
        return Cache.EditCatalogAsync(req);
    }

    /// <summary>
    ///     编辑字典内容
    /// </summary>
    [Transaction]
    public Task<QueryDicContentRsp> EditContentAsync(EditDicContentReq req)
    {
        return Cache.EditContentAsync(req);
    }

    /// <summary>
    ///     导出字典内容
    /// </summary>
    public Task<IActionResult> ExportContentAsync(QueryReq<QueryDicContentReq> req)
    {
        return Cache.ExportContentAsync(req);
    }

    /// <summary>
    ///     获取单个字典目录
    /// </summary>
    public Task<QueryDicCatalogRsp> GetCatalogAsync(QueryDicCatalogReq req)
    {
        return Cache.GetCatalogAsync(req);
    }

    /// <summary>
    ///     获取单个字典内容
    /// </summary>
    public Task<QueryDicContentRsp> GetContentAsync(QueryDicContentReq req)
    {
        return Cache.GetContentAsync(req);
    }

    /// <summary>
    ///     获取字典值
    /// </summary>
    public Task<string> GetDicValueAsync(GetDicValueReq req)
    {
        return Cache.GetDicValueAsync(req);
    }

    /// <summary>
    ///     分页查询字典目录
    /// </summary>
    public Task<PagedQueryRsp<QueryDicCatalogRsp>> PagedQueryCatalogAsync(PagedQueryReq<QueryDicCatalogReq> req)
    {
        return Cache.PagedQueryCatalogAsync(req);
    }

    /// <summary>
    ///     分页查询字典内容
    /// </summary>
    public Task<PagedQueryRsp<QueryDicContentRsp>> PagedQueryContentAsync(PagedQueryReq<QueryDicContentReq> req)
    {
        return Cache.PagedQueryContentAsync(req);
    }

    /// <summary>
    ///     查询字典目录
    /// </summary>
    public Task<IEnumerable<QueryDicCatalogRsp>> QueryCatalogAsync(QueryReq<QueryDicCatalogReq> req)
    {
        return Cache.QueryCatalogAsync(req);
    }

    /// <summary>
    ///     查询字典内容
    /// </summary>
    public Task<IEnumerable<QueryDicContentRsp>> QueryContentAsync(QueryReq<QueryDicContentReq> req)
    {
        return Cache.QueryContentAsync(req);
    }

    /// <summary>
    ///     启用/禁用字典内容
    /// </summary>
    public Task<int> SetEnabledAsync(SetDicContentEnabledReq req)
    {
        return Cache.SetEnabledAsync(req);
    }
}