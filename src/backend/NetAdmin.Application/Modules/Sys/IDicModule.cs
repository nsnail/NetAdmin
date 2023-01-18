using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Catalog;
using NetAdmin.Domain.Dto.Sys.Dic.Content;

namespace NetAdmin.Application.Modules.Sys;

/// <summary>
///     字典模块
/// </summary>
public interface IDicModule
{
    /// <summary>
    ///     批量删除字典目录
    /// </summary>
    Task<int> BulkDeleteCatalog(BulkReq<DelReq> req);

    /// <summary>
    ///     批量删除字典内容
    /// </summary>
    Task<int> BulkDeleteContent(BulkReq<DelReq> req);

    /// <summary>
    ///     创建字典目录
    /// </summary>
    Task<QueryDicCatalogRsp> CreateCatalog(CreateDicCatalogReq req);

    /// <summary>
    ///     创建字典内容
    /// </summary>
    Task<QueryDicContentRsp> CreateContent(CreateDicContentReq req);

    /// <summary>
    ///     删除字典目录
    /// </summary>
    Task<int> DeleteCatalog(DelReq req);

    /// <summary>
    ///     删除字典内容
    /// </summary>
    Task<int> DeleteContent(DelReq req);

    /// <summary>
    ///     分页查询字典目录
    /// </summary>
    Task<PagedQueryRsp<QueryDicCatalogRsp>> PagedQueryCatalog(PagedQueryReq<QueryDicCatalogReq> req);

    /// <summary>
    ///     分页查询字典内容
    /// </summary>
    Task<PagedQueryRsp<QueryDicContentRsp>> PagedQueryContent(PagedQueryReq<QueryDicContentReq> req);

    /// <summary>
    ///     查询字典目录
    /// </summary>
    Task<IEnumerable<QueryDicCatalogRsp>> QueryCatalog(QueryReq<QueryDicCatalogReq> req);

    /// <summary>
    ///     查询字典内容
    /// </summary>
    Task<IEnumerable<QueryDicContentRsp>> QueryContent(QueryReq<QueryDicContentReq> req);

    /// <summary>
    ///     更新字典目录
    /// </summary>
    Task<QueryDicCatalogRsp> UpdateCatalog(UpdateDicCatalogReq req);

    /// <summary>
    ///     更新字典内容
    /// </summary>
    Task<QueryDicContentRsp> UpdateContent(UpdateDicContentReq req);
}