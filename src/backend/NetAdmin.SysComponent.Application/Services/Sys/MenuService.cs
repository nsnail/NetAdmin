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

    /// <inheritdoc />
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await DeleteAsync(item);
        }

        return sum;
    }

    /// <inheritdoc />
    public async Task<QueryMenuRsp> CreateAsync(CreateMenuReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryMenuRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<bool> ExistAsync(QueryReq<QueryMenuReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<QueryMenuRsp> GetAsync(QueryMenuReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<PagedQueryRsp<QueryMenuRsp>> PagedQueryAsync(PagedQueryReq<QueryMenuReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryMenuRsp>> QueryAsync(QueryReq<QueryMenuReq> req)
    {
        var ret = await QueryInternal(req).ToTreeListAsync();
        return ret.Adapt<IEnumerable<QueryMenuRsp>>();
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminUnexpectedException">NetAdminUnexpectedException</exception>
    public async Task<QueryMenuRsp> UpdateAsync(UpdateMenuReq req)
    {
        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            throw new NetAdminUnexpectedException();
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
                                          DynamicFilter = new DynamicFilterInfo {
                                                              Field    = nameof(QueryMenuReq.Id)
                                                            , Operator = DynamicFilterOperator.Any
                                                            , Value    = ownedMenuIds
                                                          }
                                      });
        }

        return await ret;
    }

    /// <inheritdoc />
    protected override Task<Sys_Menu> UpdateForSqliteAsync(Sys_Menu req)
    {
        throw new NotImplementedException();
    }

    private ISelect<Sys_Menu> QueryInternal(QueryReq<QueryMenuReq> req)
    {
        return Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                  .WhereDynamic(req.Filter)
                  .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending)
                  .OrderByDescending(a => a.Sort)
                  .OrderBy(a => a.Name)
                  .OrderBy(a => a.Id);
    }
}