using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Menu;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IMenuService" />
public sealed class MenuService(DefaultRepository<Sys_Menu> rpo, IUserService userService) //
    : RepositoryService<Sys_Menu, IMenuService>(rpo), IMenuService
{
    /// <inheritdoc />
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        req.ThrowIfInvalid();
        var ret = 0;

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var item in req.Items) {
            ret += await DeleteAsync(item).ConfigureAwait(false);
        }

        return ret;
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryMenuReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).CountAsync();
    }

    /// <inheritdoc />
    public async Task<QueryMenuRsp> CreateAsync(CreateMenuReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QueryMenuRsp>();
    }

    /// <inheritdoc />
    public async Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.DeleteAsync(a => a.Id == req.Id).ConfigureAwait(false);
        _ = await Rpo.Orm.Delete<Sys_RoleMenu>()
                     .Where(a => a.MenuId == req.Id)
                     .ExecuteAffrowsAsync()
                     .ConfigureAwait(false);
        return ret;
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryMenuReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).AnyAsync();
    }

    /// <inheritdoc />
    public async Task<QueryMenuRsp> GetAsync(QueryMenuReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryMenuReq> { Filter = req }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryMenuRsp>();
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryMenuRsp>> PagedQueryAsync(PagedQueryReq<QueryMenuReq> req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryMenuRsp>> QueryAsync(QueryReq<QueryMenuReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).ToTreeListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryMenuRsp>>();
    }

    /// <inheritdoc />
    public async Task<QueryMenuRsp> UpdateAsync(UpdateMenuReq req)
    {
        req.ThrowIfInvalid();
        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync().ConfigureAwait(false) <= 0) {
            return null;
        }

        var ret = await Rpo.Where(a => a.Id == req.Id).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryMenuRsp>();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryMenuRsp>> UserMenusAsync()
    {
        var                             userInfo = await userService.UserInfoAsync().ConfigureAwait(false);
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

            var df = new DynamicFilterInfo {
                                               Field    = nameof(QueryMenuReq.Id)
                                             , Operator = DynamicFilterOperators.Any
                                             , Value    = ownedMenuIds
                                           };
            ret = QueryAsync(req with { DynamicFilter = df });
        }

        return await ret.ConfigureAwait(false);
    }

    /// <inheritdoc />
    protected override Task<Sys_Menu> UpdateForSqliteAsync(Sys_Menu req)
    {
        throw new NotImplementedException();
    }

    private ISelect<Sys_Menu> QueryInternal(QueryReq<QueryMenuReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter);
        return req.Order == Orders.Random
            ? ret.OrderByRandom()
            : ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending)
                 .OrderByDescending(a => a.Sort)
                 .OrderBy(a => a.Name)
                 .OrderBy(a => a.Id);
    }
}