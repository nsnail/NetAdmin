using NetAdmin.Application.Modules.Sys.Dic;
using NetAdmin.Application.Services.Sys.Dependency.Dic;
using NetAdmin.DataContract.Dto.Sys.Dic.Catalog;
using NetAdmin.DataContract.Dto.Sys.Dic.Content;
using NetAdmin.Host.Aop;

namespace NetAdmin.Host.WebApi.Sys;

/// <summary>
///     字典服务
/// </summary>
public class DicController : ControllerBase<IDicService>, IDicModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DicController" /> class.
    /// </summary>
    public DicController(IDicService service) //
        : base(service) { }

    /// <summary>
    ///     批量删除字典内容
    /// </summary>
    [Transaction]
    public async ValueTask<int> BulkDeleteContent(BulkDelReq req)
    {
        return await Service.BulkDeleteContent(req);
    }

    /// <summary>
    ///     创建字典目录
    /// </summary>
    [Transaction]
    public async ValueTask<QueryDicCatalogRsp> CreateCatalog(CreateDicCatalogReq req)
    {
        return await Service.CreateCatalog(req);
    }

    /// <summary>
    ///     创建字典内容
    /// </summary>
    [Transaction]
    public async ValueTask<QueryDicContentRsp> CreateContent(CreateDicContentReq req)
    {
        return await Service.CreateContent(req);
    }

    /// <summary>
    ///     删除字典目录
    /// </summary>
    [Transaction]
    public async ValueTask<int> DeleteCatalog(DelReq req)
    {
        return await Service.DeleteCatalog(req);
    }

    /// <summary>
    ///     删除字典内容
    /// </summary>
    public async ValueTask<int> DeleteContent(DelReq req)
    {
        return await Service.DeleteContent(req);
    }

    /// <summary>
    ///     分页查询字典目录
    /// </summary>
    public async ValueTask<PagedQueryRsp<QueryDicCatalogRsp>> PagedQueryCatalog(PagedQueryReq<QueryDicCatalogReq> req)
    {
        return await Service.PagedQueryCatalog(req);
    }

    /// <summary>
    ///     分页查询字典内容
    /// </summary>
    public async ValueTask<PagedQueryRsp<QueryDicContentRsp>> PagedQueryContent(PagedQueryReq<QueryDicContentReq> req)
    {
        return await Service.PagedQueryContent(req);
    }

    /// <summary>
    ///     查询字典目录
    /// </summary>
    public async ValueTask<List<QueryDicCatalogRsp>> QueryCatalog(QueryReq<QueryDicCatalogReq> req)
    {
        return await Service.QueryCatalog(req);
    }

    /// <summary>
    ///     查询字典内容
    /// </summary>
    public async ValueTask<List<QueryDicContentRsp>> QueryContent(QueryReq<QueryDicContentReq> req)
    {
        return await Service.QueryContent(req);
    }

    /// <summary>
    ///     更新字典目录
    /// </summary>
    [Transaction]
    public async ValueTask<QueryDicCatalogRsp> UpdateCatalog(UpdateDicCatalogReq req)
    {
        return await Service.UpdateCatalog(req);
    }

    /// <summary>
    ///     更新字典内容
    /// </summary>
    [Transaction]
    public async ValueTask<QueryDicContentRsp> UpdateContent(UpdateDicContentReq req)
    {
        return await Service.UpdateContent(req);
    }
}