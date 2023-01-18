using Microsoft.AspNetCore.Mvc;
using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Menu;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Caches.Sys;

namespace NetAdmin.Host.WebApi.Sys;

/// <summary>
///     菜单服务
/// </summary>
public class MenuController : ControllerBase<IMenuService>, IMenuModule
{
    private readonly IUserCache _userCache;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MenuController" /> class.
    /// </summary>
    public MenuController(IMenuService service, IUserCache userCache) //
        : base(service)
    {
        _userCache = userCache;
    }

    /// <summary>
    ///     批量删除菜单
    /// </summary>
    [Transaction]
    public async Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        return await Service.BulkDelete(req);
    }

    /// <summary>
    ///     创建菜单
    /// </summary>
    [Transaction]
    public async Task<QueryMenuRsp> Create(CreateMenuReq req)
    {
        return await Service.Create(req);
    }

    /// <summary>
    ///     删除菜单
    /// </summary>
    [Transaction]
    public async Task<int> Delete(DelReq req)
    {
        return await Service.Delete(req);
    }

    /// <summary>
    ///     分页查询菜单
    /// </summary>
    [NonAction]
    public async Task<PagedQueryRsp<QueryMenuRsp>> PagedQuery(PagedQueryReq<QueryMenuReq> req)
    {
        return await Service.PagedQuery(req);
    }

    /// <summary>
    ///     查询菜单
    /// </summary>
    public async Task<IEnumerable<QueryMenuRsp>> Query(QueryReq<QueryMenuReq> req)
    {
        return await Service.Query(req);
    }

    /// <summary>
    ///     更新菜单
    /// </summary>
    [Transaction]
    public async Task<QueryMenuRsp> Update(UpdateMenuReq req)
    {
        return await Service.Update(req);
    }

    /// <summary>
    ///     当前用户菜单
    /// </summary>
    public async Task<IEnumerable<QueryMenuRsp>> UserMenus()
    {
        return await Service.UserMenus(await _userCache.UserInfo());
    }
}