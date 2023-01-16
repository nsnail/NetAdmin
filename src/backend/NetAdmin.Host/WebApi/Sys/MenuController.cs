using Microsoft.AspNetCore.Mvc;
using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.DataContract.Dto.Sys.Menu;
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
    public async ValueTask<int> BulkDelete(BulkDelReq req)
    {
        return await Service.BulkDelete(req);
    }

    /// <summary>
    ///     创建菜单
    /// </summary>
    public async ValueTask<QueryMenuRsp> Create(CreateMenuReq req)
    {
        return await Service.Create(req);
    }

    /// <summary>
    ///     删除菜单
    /// </summary>
    public async ValueTask<int> Delete(DelReq req)
    {
        return await Service.Delete(req);
    }

    /// <summary>
    ///     分页查询菜单
    /// </summary>
    [NonAction]
    public ValueTask<PagedQueryRsp<QueryMenuRsp>> PagedQuery(PagedQueryReq<QueryMenuReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     查询菜单
    /// </summary>
    public async ValueTask<List<QueryMenuRsp>> Query(QueryReq<QueryMenuReq> req)
    {
        return await Service.Query(req);
    }

    /// <summary>
    ///     更新菜单
    /// </summary>
    public async ValueTask<QueryMenuRsp> Update(UpdateMenuReq req)
    {
        return await Service.Update(req);
    }

    /// <summary>
    ///     当前用户菜单
    /// </summary>
    public async ValueTask<List<QueryMenuRsp>> UserMenus()
    {
        return await Service.UserMenus(await _userCache.UserInfo());
    }
}