using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Menu;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Caches.Sys;

namespace NetAdmin.Host.Controllers.Sys;

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
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     创建菜单
    /// </summary>
    [Transaction]
    public Task<QueryMenuRsp> CreateAsync(CreateMenuReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <summary>
    ///     删除菜单
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <summary>
    ///     分页查询菜单
    /// </summary>
    [NonAction]
    public Task<PagedQueryRsp<QueryMenuRsp>> PagedQueryAsync(PagedQueryReq<QueryMenuReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询菜单
    /// </summary>
    public Task<IEnumerable<QueryMenuRsp>> QueryAsync(QueryReq<QueryMenuReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <summary>
    ///     更新菜单
    /// </summary>
    [Transaction]
    public Task<QueryMenuRsp> UpdateAsync(UpdateMenuReq req)
    {
        return Service.UpdateAsync(req);
    }

    /// <summary>
    ///     当前用户菜单
    /// </summary>
    public async Task<IEnumerable<QueryMenuRsp>> UserMenusAsync()
    {
        return await Service.UserMenusAsync(await _userCache.UserInfoAsync());
    }
}