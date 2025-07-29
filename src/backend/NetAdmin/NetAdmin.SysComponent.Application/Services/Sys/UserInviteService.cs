using NetAdmin.Application.Extensions;
using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.Domain.Dto.Sys.Dic.Catalog;
using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserInvite;
using NetAdmin.Domain.Dto.Sys.UserRole;
using NetAdmin.Domain.Extensions;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IUserInviteService" />
public sealed class UserInviteService(BasicRepository<Sys_UserInvite, long> rpo) //
    : RepositoryService<Sys_UserInvite, long, IUserInviteService>(rpo), IUserInviteService
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
    public Task<long> CountAsync(QueryReq<QueryUserInviteReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryUserInviteReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        .WithNoLockNoWait()
                        .GroupBy(req.GetToListExp<Sys_UserInvite>())
                        .ToDictionaryAsync(a => a.Count())
                        .ConfigureAwait(false);
        return ret.Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                              req.RequiredFields.ToImmutableDictionary(
                                  y => y, y => typeof(Sys_UserInvite).GetProperty(y)!.GetValue(x.Key)?.ToString()), x.Value))
                  .Where(x => x.Key.Any(y => !y.Value.NullOrEmpty()))
                  .OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<QueryUserInviteRsp> CreateAsync(CreateUserInviteReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QueryUserInviteRsp>();
    }

    /// <inheritdoc />
    public async Task<QueryUserRsp> CreateFansAccountAsync(CreateFansAccountReq req)
    {
        req.ThrowIfInvalid();

        var rolesAllowApply = (await QueryRolesAllowApplyAsync().ConfigureAwait(false)).Select(x => x.Value);
        return !req.RoleIds.All(x => rolesAllowApply.Contains(x.ToInvString()))
            ? throw new NetAdminInvalidOperationException(Ln.此操作不被允许)
            : await S<IUserService>()
                    .CreateAsync(req with { Invite = new CreateUserInviteReq { OwnerId = UserToken.Id, OwnerDeptId = UserToken.DeptId } })
                    .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public async Task<QueryUserInviteRsp> EditAsync(EditUserInviteReq req)
    {
        req.ThrowIfInvalid();
        #if DBTYPE_SQLSERVER
        return (await UpdateReturnListAsync(req).ConfigureAwait(false)).FirstOrDefault()?.Adapt<QueryUserInviteRsp>();
        #else
        return await UpdateAsync(req).ConfigureAwait(false) > 0 ? await GetAsync(new QueryUserInviteReq { Id = req.Id }).ConfigureAwait(false) : null;
        #endif
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryUserInviteReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QueryUserInviteReq, QueryUserInviteRsp>(QueryInternal, req, Ln.用户邀请导出);
    }

    /// <inheritdoc />
    public Task<List<long>> GetAssociatedUserIdAsync(long userId, bool up = true)
    {
        return Rpo.Orm.Select<Sys_UserInvite>()
                  .DisableGlobalFilter(Chars.FLG_FREE_SQL_GLOBAL_FILTER_SELF, Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT
                                     , Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT_WITH_CHILDREN, Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT_WITH_SON)
                  .Where(a => a.Id == userId)
                  .AsTreeCte( //
                      up: up
                    , disableGlobalFilters: [
                          Chars.FLG_FREE_SQL_GLOBAL_FILTER_SELF, Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT
                        , Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT_WITH_CHILDREN
                        , Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT_WITH_SON
                      ])
                  .ToListAsync(a => a.Id);
    }

    /// <inheritdoc />
    public async Task<QueryUserInviteRsp> GetAsync(QueryUserInviteReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryUserInviteReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryUserInviteRsp>();
    }

    /// <inheritdoc />
    public Task<bool> GetSelfRechargeAllowedAsync()
    {
        return QueryInternal(new QueryReq<QueryUserInviteReq> { Filter = new QueryUserInviteReq { Id = UserToken.Id } })
               .DisableGlobalFilter(Chars.FLG_FREE_SQL_GLOBAL_FILTER_SELF, Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT
                                  , Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT_WITH_CHILDREN, Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT_WITH_SON)
               .ToOneAsync(a => a.SelfRechargeAllowed);
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryUserInviteRsp>> PagedQueryAsync(PagedQueryReq<QueryUserInviteReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .WithNoLockNoWait()
                         .Count(out var total)
                         .ToListAsync(req)
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryUserInviteRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryUserInviteRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryUserInviteRsp>> QueryAsync(QueryReq<QueryUserInviteReq> req)
    {
        req.ThrowIfInvalid();
        var query = QueryInternal(req).Include(a => a.Owner).IncludeMany(a => a.User.Roles).WithNoLockNoWait();
        var ret = req.Filter?.IsPlainQuery == true
            ? await query.ToListAsync().ConfigureAwait(false)
            : await query.ToTreeListAsync().ConfigureAwait(false);

        return ret.Adapt<IEnumerable<QueryUserInviteRsp>>();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryDicContentRsp>> QueryRolesAllowApplyAsync()
    {
        var dicService = S<IDicService>();
        var catalogIds = (await dicService.QueryCatalogAsync(new QueryReq<QueryDicCatalogReq> {
                                                                                                  DynamicFilter = new DynamicFilterInfo {
                                                                                                      Field    = nameof(QueryDicCatalogReq.Code)
                                                                                                    , Operator = DynamicFilterOperators.Any
                                                                                                    , Value = S<ContextUserInfo>()
                                                                                                          .Roles
                                                                                                          .Select(x =>
                                                                                                              $"{Chars.FLG_DIC_CATALOG_NEW_USER_ROLE_CONFIG}>{x.Id}")
                                                                                                  }
                                                                                              })
                                          .ConfigureAwait(false)).Select(x => x.Id);
        return !catalogIds.Any()
            ? null
            : await dicService
                    .QueryContentAsync(new QueryReq<QueryDicContentReq> {
                                                                            DynamicFilter = new DynamicFilterInfo {
                                                                                                Field    = nameof(QueryDicContentReq.CatalogId)
                                                                                              , Operator = DynamicFilterOperators.Any
                                                                                              , Value    = catalogIds
                                                                                            }
                                                                        })
                    .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public Task<int> SetCommissionRatioAsync(SetCommissionRatioReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req with { CommissionRatio = req.CommissionRatio }, [nameof(req.CommissionRatio)], null, a => a.Id == req.Id, null, true);
    }

    /// <inheritdoc />
    public async Task<int> SetFansRoleAsync(SetFansRoleReq req)
    {
        req.ThrowIfInvalid();

        var rolesAllowApply = (await QueryRolesAllowApplyAsync().ConfigureAwait(false)).Select(x => x.Value).ToList();
        if (!rolesAllowApply.Contains(req.RoleId.ToInvString())) {
            throw new NetAdminInvalidOperationException(Ln.此操作不被允许);
        }

        var userRoleService = App.GetService<IUserRoleService>();
        _ = await userRoleService.BulkDeleteByUserIdAsync(req.Id).ConfigureAwait(false);
        _ = await userRoleService.CreateAsync(new CreateUserRoleReq { RoleId = req.RoleId, UserId = req.Id }).ConfigureAwait(false);
        return 1;
    }

    /// <inheritdoc />
    public async Task<int> SetInviterAsync(SetInviterReq req)
    {
        req.ThrowIfInvalid();

        var userService = S<IUserService>();
        var child       = await userService.GetAsync(new QueryUserReq { Id = req.Id }).ConfigureAwait(false);
        var parent      = await userService.GetAsync(new QueryUserReq { Id = req.OwnerId!.Value }).ConfigureAwait(false);

        // 不能将上级设置为自己的下级避免死循环
        if ((await GetAssociatedUserIdAsync(req.Id, false).ConfigureAwait(false)).Contains(req.OwnerId!.Value)) {
            throw new NetAdminInvalidOperationException(Ln.指定的上级为该账号的下级用户);
        }

        // 修改部门层级关系
        var dept = await S<IDeptService>().GetAsync(new QueryDeptReq { Id = child.DeptId }).ConfigureAwait(false);
        if (dept.Id == req.Id && dept.Id != parent.DeptId) {
            _ = await S<IDeptService>().EditAsync(dept.Adapt<EditDeptReq>() with { ParentId = parent.DeptId }).ConfigureAwait(false);
        }

        return await UpdateAsync( //
                new Sys_UserInvite { Id = req.Id, Version = req.Version, OwnerId = req.OwnerId!.Value, OwnerDeptId = parent.DeptId }
              , [nameof(req.OwnerDeptId), nameof(req.OwnerId)])
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public Task<int> SetSelfRechargeAllowedAsync(SetSelfRechargeAllowedReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req, [nameof(req.SelfRechargeAllowed)]);
    }

    /// <inheritdoc />
    public Task<decimal> SumAsync(QueryReq<QueryUserInviteReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req with { Order = Orders.None }).WithNoLockNoWait().SumAsync(req.GetSumExp<Sys_UserInvite>());
    }

    private ISelect<Sys_UserInvite> QueryInternal(QueryReq<QueryUserInviteReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereIf( //
                         req.Filter?.Id > 0, a => a.Id == req.Filter.Id);

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (req.Order) {
            case Orders.None:
                return ret.AppendOtherFilters(req);
            case Orders.Random:
                return ret.OrderByRandom().AppendOtherFilters(req);
        }

        ret = ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);
        if (!req.Prop?.Equals(nameof(req.Filter.Id), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Id);
        }

        return ret.AppendOtherFilters(req);
    }
}