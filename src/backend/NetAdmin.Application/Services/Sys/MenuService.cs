using FreeSql.Internal.Model;
using Furion.FriendlyException;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.DataContract.DbMaps.Sys;
using NetAdmin.DataContract.Dto.Sys.Menu;
using NetAdmin.DataContract.Dto.Sys.User;
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

    /// <inheritdoc />
    public async ValueTask<int> BulkDelete(BulkDelReq req)
    {
        var sum = 0;
        foreach (var id in req.Ids) {
            sum += await Delete(new DelReq { Id = id });
        }

        return sum;
    }

    /// <summary>
    ///     创建菜单
    /// </summary>
    public async ValueTask<QueryMenuRsp> Create(CreateMenuReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryMenuRsp>();
    }

    /// <summary>
    ///     删除菜单
    /// </summary>
    public async ValueTask<int> Delete(DelReq req)
    {
        var ret = await Rpo.DeleteAsync(a => a.Id == req.Id);
        return ret;
    }

    /// <inheritdoc />
    public ValueTask<PagedQueryRsp<QueryMenuRsp>> PagedQuery(PagedQueryReq<QueryMenuReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     查询菜单
    /// </summary>
    public async ValueTask<List<QueryMenuRsp>> Query(QueryReq<QueryMenuReq> req)
    {
        var ret = await Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter).ToTreeListAsync();
        return ret.ConvertAll(x => x.Adapt<QueryMenuRsp>());
    }

    /// <summary>
    ///     更新菜单
    /// </summary>
    public async ValueTask<QueryMenuRsp> Update(UpdateMenuReq req)
    {
        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            throw Oops.Oh(Enums.ErrorCodes.Unknown);
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryMenuRsp>();
    }

    /// <inheritdoc />
    public async ValueTask<List<QueryMenuRsp>> UserMenus()
    {
        var userInfo = await _userService.UserInfo();
        return await UserMenus(userInfo);
    }

    /// <inheritdoc />
    public async ValueTask<List<QueryMenuRsp>> UserMenus(QueryUserRsp userInfo)
    {
        var req = new QueryReq<QueryMenuReq>();

        if (userInfo.Roles.Any(x => x.IgnorePermissionControl)) { // 忽略权限控制
            return await Query(req);
        }

        var ownedMenuIds = userInfo.Roles.SelectMany(x => x.MenuIds);
        if (ownedMenuIds.NullOrEmpty()) {
            ownedMenuIds = new[] { 0L };
        }

        req.DynamicFilter = new DynamicFilterInfo {
                                                      Field    = nameof(QueryMenuReq.Id)
                                                    , Operator = DynamicFilterOperator.Any
                                                    , Value    = ownedMenuIds
                                                  };
        var ret = await Query(req);
        return ret;
    }
}