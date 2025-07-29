using NetAdmin.Application.Extensions;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.UserWallet;
using NetAdmin.Domain.Extensions;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IUserWalletService" />
public sealed class UserWalletService(BasicRepository<Sys_UserWallet, long> rpo) //
    : RepositoryService<Sys_UserWallet, long, IUserWalletService>(rpo), IUserWalletService
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
    public Task<long> CountAsync(QueryReq<QueryUserWalletReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryUserWalletReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        .WithNoLockNoWait()
                        .GroupBy(req.GetToListExp<Sys_UserWallet>())
                        .ToDictionaryAsync(a => a.Count())
                        .ConfigureAwait(false);
        return ret.Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                              req.RequiredFields.ToImmutableDictionary(
                                  y => y, y => typeof(Sys_UserWallet).GetProperty(y)!.GetValue(x.Key)?.ToString()), x.Value))
                  .Where(x => x.Key.Any(y => !y.Value.NullOrEmpty()))
                  .OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<QueryUserWalletRsp> CreateAsync(CreateUserWalletReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QueryUserWalletRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public async Task<QueryUserWalletRsp> EditAsync(EditUserWalletReq req)
    {
        req.ThrowIfInvalid();
        #if DBTYPE_SQLSERVER
        return (await UpdateReturnListAsync(req, [nameof(req.FrozenBalance), nameof(req.AvailableBalance)]).ConfigureAwait(false)).FirstOrDefault()
            ?.Adapt<QueryUserWalletRsp>();
        #else
        return await UpdateAsync(req).ConfigureAwait(false) > 0 ? await GetAsync(new QueryUserWalletReq { Id = req.Id }).ConfigureAwait(false) : null;
        #endif
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryUserWalletReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QueryUserWalletReq, QueryUserWalletRsp>(QueryInternal, req, Ln.用户钱包导出);
    }

    /// <inheritdoc />
    public async Task<QueryUserWalletRsp> GetAsync(QueryUserWalletReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryUserWalletReq> { Filter = req, Order = Orders.None })
                        .Include(a => a.Owner)
                        .ToOneAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<QueryUserWalletRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryUserWalletRsp>> PagedQueryAsync(PagedQueryReq<QueryUserWalletReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .IncludeMany(a => a.Owner.Roles)
                         .Include(a => a.Owner.Invite.Owner)
                         .Page(req.Page, req.PageSize)
                         .WithNoLockNoWait()
                         .Count(out var total)
                         .ToListAsync(req)
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryUserWalletRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryUserWalletRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryUserWalletRsp>> QueryAsync(QueryReq<QueryUserWalletReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().Take(req.Count).ToListAsync(req).ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryUserWalletRsp>>();
    }

    /// <inheritdoc />
    public Task<decimal> SumAsync(QueryReq<QueryUserWalletReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req with { Order = Orders.None }).WithNoLockNoWait().SumAsync(req.GetSumExp<Sys_UserWallet>());
    }

    private ISelect<Sys_UserWallet> QueryInternal(QueryReq<QueryUserWalletReq> req)
    {
        IEnumerable<long> deptIds = null;
        if (req.Filter?.DeptId > 0) {
            deptIds = Rpo.Orm.Select<Sys_Dept>().Where(a => a.Id == req.Filter.DeptId).AsTreeCte().ToList(a => a.Id);
        }

        return QueryInternal(req, deptIds);
    }

    private ISelect<Sys_UserWallet> QueryInternal(QueryReq<QueryUserWalletReq> req, IEnumerable<long> deptIds)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereIf(req.Filter?.Id > 0,     a => a.Id == req.Filter.Id)
                     .WhereIf(deptIds        != null, a => deptIds.Contains(a.Owner.DeptId))
                     .WhereIf( //
                         req.Filter?.RoleId > 0, a => a.Owner.Roles.Any(b => b.Id == req.Filter.RoleId));

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