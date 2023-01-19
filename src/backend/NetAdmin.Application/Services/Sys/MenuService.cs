using FreeSql.Internal.Model;
using Furion.FriendlyException;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Menu;
using NetAdmin.Domain.Dto.Sys.User;
using NSExt.Extensions;

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
    public async Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await Delete(item);
        }

        return sum;
    }

    /// <summary>
    ///     创建菜单
    /// </summary>
    public async Task<QueryMenuRsp> Create(CreateMenuReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryMenuRsp>();
    }

    /// <summary>
    ///     删除菜单
    /// </summary>
    public async Task<int> Delete(DelReq req)
    {
        var ret = await Rpo.DeleteAsync(a => a.Id == req.Id);
        return ret;
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryMenuRsp>> PagedQuery(PagedQueryReq<QueryMenuReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     查询菜单
    /// </summary>
    public async Task<IEnumerable<QueryMenuRsp>> Query(QueryReq<QueryMenuReq> req)
    {
        var ret = await Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter).ToTreeListAsync();
        return ret.Adapt<IEnumerable<QueryMenuRsp>>();
    }

    /// <summary>
    ///     更新菜单
    /// </summary>
    public async Task<QueryMenuRsp> Update(UpdateMenuReq req)
    {
        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation);
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryMenuRsp>();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryMenuRsp>> UserMenus()
    {
        var userInfo = await _userService.UserInfo();
        return await UserMenus(userInfo);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryMenuRsp>> UserMenus(QueryUserRsp userInfo)
    {
        var req = new QueryReq<QueryMenuReq>();

        if (userInfo.Roles.Any(x => x.IgnorePermissionControl)) { // 忽略权限控制
            return await Query(req);
        }

        var ownedMenuIds = userInfo.Roles.SelectMany(x => x.MenuIds);
        if (ownedMenuIds.NullOrEmpty()) {
            ownedMenuIds = new[] { 0L };
        }

        var ret = await Query(req with {
                                           DynamicFilter
                                           = new DynamicFilterInfo {
                                                                       Field    = nameof(QueryMenuReq.Id)
                                                                     , Operator = DynamicFilterOperator.Any
                                                                     , Value    = ownedMenuIds
                                                                   }
                                       });
        return ret;
    }
}