using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Catalog;
using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     字典服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class DicController : ControllerBase<IDicCache, IDicService>, IDicModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DicController" /> class.
    /// </summary>
    public DicController(IDicCache cache) //
        : base(cache) { }

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
    ///     更新字典目录
    /// </summary>
    [Transaction]
    public Task<QueryDicCatalogRsp> UpdateCatalogAsync(UpdateDicCatalogReq req)
    {
        return Cache.UpdateCatalogAsync(req);
    }

    /// <summary>
    ///     更新字典内容
    /// </summary>
    [Transaction]
    public Task<QueryDicContentRsp> UpdateContentAsync(UpdateDicContentReq req)
    {
        return Cache.UpdateContentAsync(req);
    }
}