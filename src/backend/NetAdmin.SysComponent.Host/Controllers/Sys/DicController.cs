using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Catalog;
using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     字典服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class DicController : ControllerBase<IDicService>, IDicModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DicController" /> class.
    /// </summary>
    public DicController(IDicService service) //
        : base(service) { }

    /// <summary>
    ///     批量删除字典目录
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteCatalogAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteCatalogAsync(req);
    }

    /// <summary>
    ///     批量删除字典内容
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteContentAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteContentAsync(req);
    }

    /// <summary>
    ///     创建字典目录
    /// </summary>
    [Transaction]
    public Task<QueryDicCatalogRsp> CreateCatalogAsync(CreateDicCatalogReq req)
    {
        return Service.CreateCatalogAsync(req);
    }

    /// <summary>
    ///     创建字典内容
    /// </summary>
    [Transaction]
    public Task<QueryDicContentRsp> CreateContentAsync(CreateDicContentReq req)
    {
        return Service.CreateContentAsync(req);
    }

    /// <summary>
    ///     删除字典目录
    /// </summary>
    [Transaction]
    public Task<int> DeleteCatalogAsync(DelReq req)
    {
        return Service.DeleteCatalogAsync(req);
    }

    /// <summary>
    ///     删除字典内容
    /// </summary>
    [Transaction]
    public Task<int> DeleteContentAsync(DelReq req)
    {
        return Service.DeleteContentAsync(req);
    }

    /// <summary>
    ///     分页查询字典目录
    /// </summary>
    public Task<PagedQueryRsp<QueryDicCatalogRsp>> PagedQueryCatalogAsync(PagedQueryReq<QueryDicCatalogReq> req)
    {
        return Service.PagedQueryCatalogAsync(req);
    }

    /// <summary>
    ///     分页查询字典内容
    /// </summary>
    public Task<PagedQueryRsp<QueryDicContentRsp>> PagedQueryContentAsync(PagedQueryReq<QueryDicContentReq> req)
    {
        return Service.PagedQueryContentAsync(req);
    }

    /// <summary>
    ///     查询字典目录
    /// </summary>
    public Task<IEnumerable<QueryDicCatalogRsp>> QueryCatalogAsync(QueryReq<QueryDicCatalogReq> req)
    {
        return Service.QueryCatalogAsync(req);
    }

    /// <summary>
    ///     查询字典内容
    /// </summary>
    public Task<IEnumerable<QueryDicContentRsp>> QueryContentAsync(QueryReq<QueryDicContentReq> req)
    {
        return Service.QueryContentAsync(req);
    }

    /// <summary>
    ///     更新字典目录
    /// </summary>
    [Transaction]
    public Task<QueryDicCatalogRsp> UpdateCatalogAsync(UpdateDicCatalogReq req)
    {
        return Service.UpdateCatalogAsync(req);
    }

    /// <summary>
    ///     更新字典内容
    /// </summary>
    [Transaction]
    public Task<QueryDicContentRsp> UpdateContentAsync(UpdateDicContentReq req)
    {
        return Service.UpdateContentAsync(req);
    }
}