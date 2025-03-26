using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Menu;
using NetAdmin.Domain.Extensions;

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
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryMenuReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        .WithNoLockNoWait()
                        .GroupBy(req.GetToListExp<Sys_Menu>())
                        .ToDictionaryAsync(a => a.Count())
                        .ConfigureAwait(false);
        return ret.Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                              req.RequiredFields.ToImmutableDictionary(y => y, y => typeof(Sys_Menu).GetProperty(y)!.GetValue(x.Key)!.ToString())
                            , x.Value))
                  .OrderByDescending(x => x.Value);
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
        _ = await Rpo.Orm.Delete<Sys_RoleMenu>().Where(a => a.MenuId == req.Id).ExecuteEffectsAsync().ConfigureAwait(false);
        return ret;
    }

    /// <inheritdoc />
    public async Task<QueryMenuRsp> EditAsync(EditMenuReq req)
    {
        req.ThrowIfInvalid();
        #if DBTYPE_SQLSERVER
        return (await UpdateReturnListAsync(req).ConfigureAwait(false)).FirstOrDefault()?.Adapt<QueryMenuRsp>();
        #else
        return await UpdateAsync(req).ConfigureAwait(false) > 0 ? await GetAsync(new QueryMenuReq { Id = req.Id }).ConfigureAwait(false) : null;
        #endif
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryMenuReq> req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<QueryMenuRsp> GetAsync(QueryMenuReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryMenuReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
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
        var ret = await QueryInternal(req).WithNoLockNoWait().ToTreeListAsync().ConfigureAwait(false);
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
                ownedMenuIds = [0L];
            }

            var df = new DynamicFilterInfo { Field = nameof(QueryMenuReq.Id), Operator = DynamicFilterOperators.Any, Value = ownedMenuIds };
            ret = QueryAsync(req with { DynamicFilter = df });
        }

        return await ret.ConfigureAwait(false);
    }

    private ISelect<Sys_Menu> QueryInternal(QueryReq<QueryMenuReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter);

        return req.Order switch {
                   Orders.None   => ret
                 , Orders.Random => ret.OrderByRandom()
                 , _ => ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending)
                           .OrderByDescending(a => a.Sort)
                           .OrderBy(a => a.Name)
                           .OrderBy(a => a.Id)
               };
    }
}