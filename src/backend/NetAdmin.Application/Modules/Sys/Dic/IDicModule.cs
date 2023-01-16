using NetAdmin.DataContract.Dto.Sys.Dic.Catalog;
using NetAdmin.DataContract.Dto.Sys.Dic.Content;

namespace NetAdmin.Application.Modules.Sys.Dic;

/// <summary>
///     字典模块
/// </summary>
public interface IDicModule
{
    /// <summary>
    ///     批量删除字典内容
    /// </summary>
    ValueTask<int> BulkDeleteContent(BulkDelReq req);

    /// <summary>
    ///     创建字典目录
    /// </summary>
    ValueTask<QueryDicCatalogRsp> CreateCatalog(CreateDicCatalogReq req);

    /// <summary>
    ///     创建字典内容
    /// </summary>
    ValueTask<QueryDicContentRsp> CreateContent(CreateDicContentReq req);

    /// <summary>
    ///     删除字典目录
    /// </summary>
    ValueTask<int> DeleteCatalog(DelReq req);

    /// <summary>
    ///     删除字典内容
    /// </summary>
    ValueTask<int> DeleteContent(DelReq req);

    /// <summary>
    ///     分页查询字典目录
    /// </summary>
    ValueTask<PagedQueryRsp<QueryDicCatalogRsp>> PagedQueryCatalog(PagedQueryReq<QueryDicCatalogReq> req);

    /// <summary>
    ///     分页查询字典内容
    /// </summary>
    ValueTask<PagedQueryRsp<QueryDicContentRsp>> PagedQueryContent(PagedQueryReq<QueryDicContentReq> req);

    /// <summary>
    ///     查询字典目录
    /// </summary>
    ValueTask<List<QueryDicCatalogRsp>> QueryCatalog(QueryReq<QueryDicCatalogReq> req);

    /// <summary>
    ///     查询字典内容
    /// </summary>
    ValueTask<List<QueryDicContentRsp>> QueryContent(QueryReq<QueryDicContentReq> req);

    /// <summary>
    ///     更新字典目录
    /// </summary>
    ValueTask<QueryDicCatalogRsp> UpdateCatalog(UpdateDicCatalogReq req);

    /// <summary>
    ///     更新字典内容
    /// </summary>
    ValueTask<QueryDicContentRsp> UpdateContent(UpdateDicContentReq req);
}