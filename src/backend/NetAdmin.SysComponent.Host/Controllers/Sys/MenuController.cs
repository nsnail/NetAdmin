using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Menu;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     菜单服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class MenuController(IMenuCache cache) : ControllerBase<IMenuCache, IMenuService>(cache), IMenuModule
{
    /// <summary>
    ///     批量删除菜单
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     菜单计数
    /// </summary>
    public Task<long> CountAsync(QueryReq<QueryMenuReq> req)
    {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     创建菜单
    /// </summary>
    [Transaction]
    public Task<QueryMenuRsp> CreateAsync(CreateMenuReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除菜单
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     编辑菜单
    /// </summary>
    [Transaction]
    public Task<QueryMenuRsp> EditAsync(EditMenuReq req)
    {
        return Cache.EditAsync(req);
    }

    /// <summary>
    ///     菜单是否存在
    /// </summary>
    [NonAction]
    public Task<bool> ExistAsync(QueryReq<QueryMenuReq> req)
    {
        return Cache.ExistAsync(req);
    }

    /// <summary>
    ///     导出菜单
    /// </summary>
    [NonAction]
    public Task<IActionResult> ExportAsync(QueryReq<QueryMenuReq> req)
    {
        return Cache.ExportAsync(req);
    }

    /// <summary>
    ///     获取单个菜单
    /// </summary>
    public Task<QueryMenuRsp> GetAsync(QueryMenuReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     分页查询菜单
    /// </summary>
    [NonAction]
    public Task<PagedQueryRsp<QueryMenuRsp>> PagedQueryAsync(PagedQueryReq<QueryMenuReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询菜单
    /// </summary>
    public Task<IEnumerable<QueryMenuRsp>> QueryAsync(QueryReq<QueryMenuReq> req)
    {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     当前用户菜单
    /// </summary>
    public Task<IEnumerable<QueryMenuRsp>> UserMenusAsync()
    {
        return Cache.UserMenusAsync();
    }
}