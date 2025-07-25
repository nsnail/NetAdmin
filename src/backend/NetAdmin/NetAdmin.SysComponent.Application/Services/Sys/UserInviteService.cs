using NetAdmin.Application.Extensions;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.UserInvite;
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
    public Task<List<long>> GetAssociatedUserIdAsync(long userId)
    {
        return Rpo.Orm.Select<Sys_UserInvite>()
                  .DisableGlobalFilter(Chars.FLG_FREE_SQL_GLOBAL_FILTER_SELF, Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT
,                                                                             Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT_WITH_CHILD)
                  .Where(a => a.Id == userId)
                  .AsTreeCte( //
                      up: true
                    , disableGlobalFilters: [
                          Chars.FLG_FREE_SQL_GLOBAL_FILTER_SELF, Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT
                        , Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT_WITH_CHILD
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
        var query = QueryInternal(req).Include(a => a.Owner).Include(a => a.User).WithNoLockNoWait();
        var ret = req.Filter?.IsPlainQuery == true
            ? await query.ToListAsync().ConfigureAwait(false)
            : await query.ToTreeListAsync().ConfigureAwait(false);

        return ret.Adapt<IEnumerable<QueryUserInviteRsp>>();
    }

    /// <inheritdoc />
    public Task<int> SetCommissionRatioAsync(SetCommissionRatioReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req with { CommissionRatio = req.CommissionRatio }, [nameof(req.CommissionRatio)], null, a => a.Id == req.Id, null, true);
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