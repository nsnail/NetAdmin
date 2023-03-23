using Furion.FriendlyException;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Menu;
using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="IMenuService" />
public class MenuService : RepositoryService<TbSysMenu, IMenuService>, IMenuService
{
    private readonly IUserService _userService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MenuService" /> class.
    /// </summary>
    public MenuService(Repository<TbSysMenu> rpo, IUserService userService) //
        : base(rpo)
    {
        _userService = userService;
    }

    /// <summary>
    ///     批量删除菜单
    /// </summary>
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await DeleteAsync(item);
        }

        return sum;
    }

    /// <summary>
    ///     创建菜单
    /// </summary>
    public async Task<QueryMenuRsp> CreateAsync(CreateMenuReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryMenuRsp>();
    }

    /// <summary>
    ///     删除菜单
    /// </summary>
    public Task<int> DeleteAsync(DelReq req)
    {
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryMenuRsp>> PagedQueryAsync(PagedQueryReq<QueryMenuReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     查询菜单
    /// </summary>
    public async Task<IEnumerable<QueryMenuRsp>> QueryAsync(QueryReq<QueryMenuReq> req)
    {
        var ret = await Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter).ToTreeListAsync();
        return ret.Adapt<IEnumerable<QueryMenuRsp>>();
    }

    /// <summary>
    ///     更新菜单
    /// </summary>
    /// <exception cref="AppFriendlyException">AppFriendlyException</exception>
    public async Task<QueryMenuRsp> UpdateAsync(UpdateMenuReq req)
    {
        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation);
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryMenuRsp>();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryMenuRsp>> UserMenusAsync()
    {
        var userInfo = await _userService.UserInfoAsync();
        return await UserMenusAsync(userInfo);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryMenuRsp>> UserMenusAsync(QueryUserRsp userInfo)
    {
        var req = new QueryReq<QueryMenuReq>();

        if (userInfo.Roles.Any(x => x.IgnorePermissionControl)) { // 忽略权限控制
            return QueryAsync(req);
        }

        var ownedMenuIds = userInfo.Roles.SelectMany(x => x.MenuIds);
        if (ownedMenuIds.NullOrEmpty()) {
            ownedMenuIds = new[] { 0L };
        }

        return QueryAsync(req with {
                                       DynamicFilter
                                       = new DynamicFilterInfo {
                                                                   Field    = nameof(QueryMenuReq.Id)
                                                                 , Operator = DynamicFilterOperator.Any
                                                                 , Value    = ownedMenuIds
                                                               }
                                   });
    }
}