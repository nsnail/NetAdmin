using NetAdmin.Domain.Dto.Sys.Doc.Catalog;
using NetAdmin.Domain.Dto.Sys.Doc.Content;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     文档服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class DocController(IDocCache cache) : ControllerBase<IDocCache, IDocService>(cache), IDocModule
{
    /// <summary>
    ///     批量删除文档分类
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteCatalogAsync(BulkReq<DelReq> req) {
        return Cache.BulkDeleteCatalogAsync(req);
    }

    /// <summary>
    ///     批量删除文档内容
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteContentAsync(BulkReq<DelReq> req) {
        return Cache.BulkDeleteContentAsync(req);
    }

    /// <summary>
    ///     文档内容分组计数
    /// </summary>
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> ContentCountByAsync(QueryReq<QueryDocContentReq> req) {
        return Cache.ContentCountByAsync(req);
    }

    /// <summary>
    ///     创建文档分类
    /// </summary>
    [Transaction]
    public Task<QueryDocCatalogRsp> CreateCatalogAsync(CreateDocCatalogReq req) {
        return Cache.CreateCatalogAsync(req);
    }

    /// <summary>
    ///     创建文档内容
    /// </summary>
    [Transaction]
    public Task<QueryDocContentRsp> CreateContentAsync(CreateDocContentReq req) {
        return Cache.CreateContentAsync(req);
    }

    /// <summary>
    ///     删除文档分类
    /// </summary>
    [Transaction]
    public Task<int> DeleteCatalogAsync(DelReq req) {
        return Cache.DeleteCatalogAsync(req);
    }

    /// <summary>
    ///     删除文档内容
    /// </summary>
    [Transaction]
    public Task<int> DeleteContentAsync(DelReq req) {
        return Cache.DeleteContentAsync(req);
    }

    /// <summary>
    ///     编辑文档分类
    /// </summary>
    [Transaction]
    public Task<QueryDocCatalogRsp> EditCatalogAsync(EditDocCatalogReq req) {
        return Cache.EditCatalogAsync(req);
    }

    /// <summary>
    ///     编辑文档内容
    /// </summary>
    [Transaction]
    public Task<QueryDocContentRsp> EditContentAsync(EditDocContentReq req) {
        return Cache.EditContentAsync(req);
    }

    /// <summary>
    ///     导出文档内容
    /// </summary>
    public Task<IActionResult> ExportContentAsync(QueryReq<QueryDocContentReq> req) {
        return Cache.ExportContentAsync(req);
    }

    /// <summary>
    ///     获取单个文档分类
    /// </summary>
    public Task<QueryDocCatalogRsp> GetCatalogAsync(QueryDocCatalogReq req) {
        return Cache.GetCatalogAsync(req);
    }

    /// <summary>
    ///     获取单个文档内容
    /// </summary>
    public Task<QueryDocContentRsp> GetContentAsync(QueryDocContentReq req) {
        return Cache.GetContentAsync(req);
    }

    /// <summary>
    ///     分页查询文档分类
    /// </summary>
    public Task<PagedQueryRsp<QueryDocCatalogRsp>> PagedQueryCatalogAsync(PagedQueryReq<QueryDocCatalogReq> req) {
        return Cache.PagedQueryCatalogAsync(req);
    }

    /// <summary>
    ///     分页查询文档内容
    /// </summary>
    public Task<PagedQueryRsp<QueryDocContentRsp>> PagedQueryContentAsync(PagedQueryReq<QueryDocContentReq> req) {
        return Cache.PagedQueryContentAsync(req);
    }

    /// <summary>
    ///     查询文档分类
    /// </summary>
    public Task<IEnumerable<QueryDocCatalogRsp>> QueryCatalogAsync(QueryReq<QueryDocCatalogReq> req) {
        return Cache.QueryCatalogAsync(req);
    }

    /// <summary>
    ///     查询文档内容
    /// </summary>
    public Task<IEnumerable<QueryDocContentRsp>> QueryContentAsync(QueryReq<QueryDocContentReq> req) {
        return Cache.QueryContentAsync(req);
    }

    /// <summary>
    ///     启用/禁用文档内容
    /// </summary>
    public Task<int> SetEnabledAsync(SetDocContentEnabledReq req) {
        return Cache.SetEnabledAsync(req);
    }

    /// <summary>
    ///     浏览文档内容
    /// </summary>
    [AllowAnonymous]
    public Task<QueryDocContentRsp> ViewContentAsync(QueryDocContentReq req) {
        return Cache.ViewContentAsync(req);
    }
}