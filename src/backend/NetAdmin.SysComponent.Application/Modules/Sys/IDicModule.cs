using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Catalog;
using NetAdmin.Domain.Dto.Sys.Dic.Content;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     字典模块
/// </summary>
public interface IDicModule
{
    /// <summary>
    ///     批量删除字典目录
    /// </summary>
    Task<int> BulkDeleteCatalogAsync(BulkReq<DelReq> req);

    /// <summary>
    ///     批量删除字典内容
    /// </summary>
    Task<int> BulkDeleteContentAsync(BulkReq<DelReq> req);

    /// <summary>
    ///     创建字典目录
    /// </summary>
    Task<QueryDicCatalogRsp> CreateCatalogAsync(CreateDicCatalogReq req);

    /// <summary>
    ///     创建字典内容
    /// </summary>
    Task<QueryDicContentRsp> CreateContentAsync(CreateDicContentReq req);

    /// <summary>
    ///     删除字典目录
    /// </summary>
    Task<int> DeleteCatalogAsync(DelReq req);

    /// <summary>
    ///     删除字典内容
    /// </summary>
    Task<int> DeleteContentAsync(DelReq req);

    /// <summary>
    ///     编辑字典目录
    /// </summary>
    Task<int> EditCatalogAsync(EditDicCatalogReq req);

    /// <summary>
    ///     编辑字典内容
    /// </summary>
    Task<QueryDicContentRsp> EditContentAsync(EditDicContentReq req);

    /// <summary>
    ///     导出字典内容
    /// </summary>
    Task<IActionResult> ExportContentAsync(QueryReq<QueryDicContentReq> req);

    /// <summary>
    ///     获取单个字典目录
    /// </summary>
    Task<QueryDicCatalogRsp> GetCatalogAsync(QueryDicCatalogReq req);

    /// <summary>
    ///     获取单个字典内容
    /// </summary>
    Task<QueryDicContentRsp> GetContentAsync(QueryDicContentReq req);

    /// <summary>
    ///     获取字典值
    /// </summary>
    Task<string> GetDicValueAsync(GetDicValueReq req);

    /// <summary>
    ///     分页查询字典目录
    /// </summary>
    Task<PagedQueryRsp<QueryDicCatalogRsp>> PagedQueryCatalogAsync(PagedQueryReq<QueryDicCatalogReq> req);

    /// <summary>
    ///     分页查询字典内容
    /// </summary>
    Task<PagedQueryRsp<QueryDicContentRsp>> PagedQueryContentAsync(PagedQueryReq<QueryDicContentReq> req);

    /// <summary>
    ///     查询字典目录
    /// </summary>
    Task<IEnumerable<QueryDicCatalogRsp>> QueryCatalogAsync(QueryReq<QueryDicCatalogReq> req);

    /// <summary>
    ///     查询字典内容
    /// </summary>
    Task<IEnumerable<QueryDicContentRsp>> QueryContentAsync(QueryReq<QueryDicContentReq> req);

    /// <summary>
    ///     启用/禁用字典内容
    /// </summary>
    Task<int> SetEnabledAsync(SetDicContentEnabledReq req);
}