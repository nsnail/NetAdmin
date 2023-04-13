using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Menu;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IMenuService" />
public sealed class MenuService : RepositoryService<Sys_Menu, IMenuService>, IMenuService
{
    private readonly IUserService _userService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MenuService" /> class.
    /// </summary>
    public MenuService(Repository<Sys_Menu> rpo, IUserService userService) //
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
    public Task<bool> ExistAsync(QueryReq<QueryMenuReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryMenuRsp> GetAsync(QueryMenuReq req)
    {
        throw new NotImplementedException();
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
        var ret = await Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                           .WhereDynamic(req.Filter)
                           .OrderByDescending(a => a.Sort)
                           .OrderBy(a => a.Title)
                           .ToTreeListAsync();
        return ret.Adapt<IEnumerable<QueryMenuRsp>>();
    }

    /// <summary>
    ///     更新菜单
    /// </summary>
    /// <exception cref="LineUnexpectedException">LineUnexpectedException</exception>
    public async Task<QueryMenuRsp> UpdateAsync(UpdateMenuReq req)
    {
        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            throw new LineUnexpectedException();
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryMenuRsp>();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryMenuRsp>> UserMenusAsync()
    {
        var                             userInfo = await _userService.UserInfoAsync();
        Task<IEnumerable<QueryMenuRsp>> ret;
        var                             req = new QueryReq<QueryMenuReq>();

        if (userInfo.Roles.Any(x => x.IgnorePermissionControl)) {
            // 忽略权限控制
            ret = QueryAsync(req);
        }
        else {
            var ownedMenuIds = userInfo.Roles.SelectMany(x => x.MenuIds);
            if (ownedMenuIds.NullOrEmpty()) {
                ownedMenuIds = new[] { 0L };
            }

            ret = QueryAsync(req with {
                                          DynamicFilter
                                          = new DynamicFilterInfo {
                                                                      Field    = nameof(QueryMenuReq.Id)
                                                                    , Operator = DynamicFilterOperator.Any
                                                                    , Value    = ownedMenuIds
                                                                  }
                                      });
        }

        return await ret;
    }
}