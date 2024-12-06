using NetAdmin.Domain.Dto.Sys.Doc.Catalog;
using NetAdmin.Domain.Dto.Sys.Doc.Content;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     文档模块
/// </summary>
public interface IDocModule
{
    /// <summary>
    ///     批量删除文档分类
    /// </summary>
    Task<int> BulkDeleteCatalogAsync(BulkReq<DelReq> req);

    /// <summary>
    ///     批量删除文档内容
    /// </summary>
    Task<int> BulkDeleteContentAsync(BulkReq<DelReq> req);

    /// <summary>
    ///     创建文档分类
    /// </summary>
    Task<QueryDocCatalogRsp> CreateCatalogAsync(CreateDocCatalogReq req);

    /// <summary>
    ///     创建文档内容
    /// </summary>
    Task<QueryDocContentRsp> CreateContentAsync(CreateDocContentReq req);

    /// <summary>
    ///     删除文档分类
    /// </summary>
    Task<int> DeleteCatalogAsync(DelReq req);

    /// <summary>
    ///     删除文档内容
    /// </summary>
    Task<int> DeleteContentAsync(DelReq req);

    /// <summary>
    ///     编辑文档分类
    /// </summary>
    Task<QueryDocCatalogRsp> EditCatalogAsync(EditDocCatalogReq req);

    /// <summary>
    ///     编辑文档内容
    /// </summary>
    Task<QueryDocContentRsp> EditContentAsync(EditDocContentReq req);

    /// <summary>
    ///     导出文档内容
    /// </summary>
    Task<IActionResult> ExportContentAsync(QueryReq<QueryDocContentReq> req);

    /// <summary>
    ///     获取单个文档分类
    /// </summary>
    Task<QueryDocCatalogRsp> GetCatalogAsync(QueryDocCatalogReq req);

    /// <summary>
    ///     获取单个文档内容
    /// </summary>
    Task<QueryDocContentRsp> GetContentAsync(QueryDocContentReq req);

    /// <summary>
    ///     分页查询文档分类
    /// </summary>
    Task<PagedQueryRsp<QueryDocCatalogRsp>> PagedQueryCatalogAsync(PagedQueryReq<QueryDocCatalogReq> req);

    /// <summary>
    ///     分页查询文档内容
    /// </summary>
    Task<PagedQueryRsp<QueryDocContentRsp>> PagedQueryContentAsync(PagedQueryReq<QueryDocContentReq> req);

    /// <summary>
    ///     查询文档分类
    /// </summary>
    Task<IEnumerable<QueryDocCatalogRsp>> QueryCatalogAsync(QueryReq<QueryDocCatalogReq> req);

    /// <summary>
    ///     查询文档内容
    /// </summary>
    Task<IEnumerable<QueryDocContentRsp>> QueryContentAsync(QueryReq<QueryDocContentReq> req);

    /// <summary>
    ///     启用/禁用文档内容
    /// </summary>
    Task<int> SetEnabledAsync(SetDocContentEnabledReq req);

    /// <summary>
    ///     浏览文档内容
    /// </summary>
    Task<QueryDocContentRsp> ViewContentAsync(QueryDocContentReq req);
}