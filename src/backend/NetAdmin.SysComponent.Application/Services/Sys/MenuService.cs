using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Menu;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IMenuService" />
public sealed class MenuService(BasicRepository<Sys_Menu, long> rpo, IUserService userService) //
    : RepositoryService<Sys_Menu, long, IMenuService>(rpo), IMenuService
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
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .CountAsync();
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
    public async Task<QueryMenuRsp> EditAsync(EditMenuReq req)
    {
        #if DBTYPE_SQLSERVER
        return (await UpdateReturnListAsync(req, null).ConfigureAwait(false)).FirstOrDefault()?.Adapt<QueryMenuRsp>();
        #else
        return await UpdateAsync(req, null).ConfigureAwait(false) > 0
            ? await GetAsync(new QueryMenuReq { Id = req.Id }).ConfigureAwait(false)
            : null;
        #endif
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryMenuReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .AnyAsync();
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
        var ret = await QueryInternal(req)
                        #if DBTYPE_SQLSERVER
                        .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                        #endif
                        .ToTreeListAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryMenuRsp>>();
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

    private ISelect<Sys_Menu> QueryInternal(QueryReq<QueryMenuReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter);
        #pragma warning disable IDE0072
        return req.Order switch {
                   Orders.None   => ret
                 , Orders.Random => ret.OrderByRandom()
                 , _ => ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending)
                           .OrderByDescending(a => a.Sort)
                           .OrderBy(a => a.Name)
                           .OrderBy(a => a.Id)
               };
        #pragma warning restore IDE0072
    }
}