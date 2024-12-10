using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Role;
using NetAdmin.Domain.Dto.Sys.UserRole;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IRoleService" />
public sealed class RoleService(BasicRepository<Sys_Role, long> rpo, IUserRoleService userRoleService) //
    : RepositoryService<Sys_Role, long, IRoleService>(rpo), IRoleService
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
    public Task<long> CountAsync(QueryReq<QueryRoleReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryRoleReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        .WithNoLockNoWait()
                        .GroupBy(req.GetToListExp<Sys_Role>())
                        .ToDictionaryAsync(a => a.Count())
                        .ConfigureAwait(false);
        return ret.Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                              req.RequiredFields.ToImmutableDictionary(y => y, y => typeof(Sys_Role).GetProperty(y)!.GetValue(x.Key)!.ToString())
                            , x.Value))
                  .OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<QueryRoleRsp> CreateAsync(CreateRoleReq req)
    {
        req.ThrowIfInvalid();
        var entity = req.Adapt<Sys_Role>();
        var ret    = await Rpo.InsertAsync(entity).ConfigureAwait(false);

        await Rpo.SaveManyAsync(entity, nameof(entity.Depts)).ConfigureAwait(false);
        await Rpo.SaveManyAsync(entity, nameof(entity.Menus)).ConfigureAwait(false);
        await Rpo.SaveManyAsync(entity, nameof(entity.Apis)).ConfigureAwait(false);

        entity = entity with { Id = ret.Id };
        return entity.Adapt<QueryRoleRsp>();
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">Users_exist_under_this_role_and_deletion_is_not_allowed</exception>
    public async Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return await Rpo.Orm.Select<Sys_UserRole>().WithNoLockNoWait().AnyAsync(a => a.RoleId == req.Id).ConfigureAwait(false)
            ? throw new NetAdminInvalidOperationException(Ln.该角色下存在用户)
            : await Rpo.DeleteAsync(a => a.Id == req.Id).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<QueryRoleRsp> EditAsync(EditRoleReq req)
    {
        req.ThrowIfInvalid();
        var entity = req.Adapt<Sys_Role>();
        _ = await Rpo.UpdateAsync(entity).ConfigureAwait(false);
        await Rpo.SaveManyAsync(entity, nameof(entity.Depts)).ConfigureAwait(false);
        await Rpo.SaveManyAsync(entity, nameof(entity.Menus)).ConfigureAwait(false);
        await Rpo.SaveManyAsync(entity, nameof(entity.Apis)).ConfigureAwait(false);

        return (await QueryAsync(new QueryReq<QueryRoleReq> { Filter = new QueryRoleReq { Id = req.Id } }).ConfigureAwait(false)).First();
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryRoleReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QueryRoleReq, ExportRoleRsp>(QueryInternal, req, Ln.角色导出);
    }

    /// <inheritdoc />
    public async Task<QueryRoleRsp> GetAsync(QueryRoleReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryRoleReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryRoleRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryRoleRsp>> PagedQueryAsync(PagedQueryReq<QueryRoleReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).WithNoLockNoWait().Count(out var total).ToListAsync().ConfigureAwait(false);

        return new PagedQueryRsp<QueryRoleRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryRoleRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryRoleRsp>> QueryAsync(QueryReq<QueryRoleReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().ToListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryRoleRsp>>();
    }

    /// <inheritdoc />
    public Task<int> SetDisplayDashboardAsync(SetDisplayDashboardReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req, [nameof(req.DisplayDashboard)]);
    }

    /// <inheritdoc />
    public Task<int> SetEnabledAsync(SetRoleEnabledReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req, [nameof(req.Enabled)]);
    }

    /// <inheritdoc />
    public Task<int> SetIgnorePermissionControlAsync(SetIgnorePermissionControlReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req, [nameof(req.IgnorePermissionControl)]);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> UserCountByAsync(QueryReq<QueryUserRoleReq> req)
    {
        req.ThrowIfInvalid();
        return userRoleService.CountByAsync(req);
    }

    private ISelect<Sys_Role> QueryInternal(QueryReq<QueryRoleReq> req)
    {
        #pragma warning disable RCS1196

        // ReSharper disable InvokeAsExtensionMethod
        var ret = Rpo.Select.IncludeMany(a => Enumerable.Select(a.Depts, b => new Sys_Dept { Id = b.Id }))
                     .IncludeMany(a => Enumerable.Select(a.Menus,        b => new Sys_Menu { Id = b.Id }))
                     .IncludeMany(a => Enumerable.Select(a.Apis,         b => new Sys_Api { Id  = b.Id }))
                     .WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .WhereIf( //
                         req.Keywords?.Length > 0
                       , a => a.Id == req.Keywords.Int64Try(0) || a.Name.Contains(req.Keywords) || a.Summary.Contains(req.Keywords));

        // ReSharper restore InvokeAsExtensionMethod
        #pragma warning restore RCS1196

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (req.Order) {
            case Orders.None:
                return ret;
            case Orders.Random:
                return ret.OrderByRandom();
        }

        ret = ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);

        if (!req.Prop?.Equals(nameof(req.Filter.Sort), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Sort);
        }

        if (!req.Prop?.Equals(nameof(req.Filter.CreatedTime), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.CreatedTime);
        }

        return ret;
    }
}