using NetAdmin.Application.Extensions;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserWallet;
using NetAdmin.Domain.Dto.Sys.WalletFrozen;
using NetAdmin.Domain.Enums.Sys;
using NetAdmin.Domain.Extensions;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IWalletFrozenService" />
public sealed class WalletFrozenService(BasicRepository<Sys_WalletFrozen, long> rpo) //
    : RepositoryService<Sys_WalletFrozen, long, IWalletFrozenService>(rpo), IWalletFrozenService
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
    public Task<long> CountAsync(QueryReq<QueryWalletFrozenReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryWalletFrozenReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        .WithNoLockNoWait()
                        .GroupBy(req.GetToListExp<Sys_WalletFrozen>())
                        .ToDictionaryAsync(a => a.Count())
                        .ConfigureAwait(false);
        return ret.Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                              req.RequiredFields.ToImmutableDictionary(
                                  y => y
                                , y => y.Contains('.')
                                      ? typeof(Sys_WalletFrozen).GetRecursiveProperty(y)!.GetValue(
                                          x.Key.GetType().GetRecursiveProperty(y[..y.LastIndexOf('.')]).GetValue(x.Key))!.ToString()
                                      : typeof(Sys_WalletFrozen).GetProperty(y)!.GetValue(x.Key)!.ToString()), x.Value))
                  .OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<QueryWalletFrozenRsp> CreateAsync(CreateWalletFrozenReq req)
    {
        req.ThrowIfInvalid();
        var userId     = UserToken.Id;
        var userDeptId = UserToken.DeptId;
        if (req.OwnerId != null) {
            userId = req.OwnerId.Value;
            var owner = await S<IUserService>().GetAsync(new QueryUserReq { Id = userId }).ConfigureAwait(false);
            userDeptId = owner.DeptId;
        }

        var wallet = await S<IUserWalletService>().GetAsync(new QueryUserWalletReq { Id = userId }).ConfigureAwait(false);
        if (wallet.AvailableBalance < req.Amount) {
            throw new NetAdminInvalidOperationException(Ln.钱包可用余额不足);
        }

        var frozenBalanceBefore = wallet.FrozenBalance;
        if (await S<IUserWalletService>()
                  .EditAsync(wallet.Adapt<EditUserWalletReq>() with {
                                                                        AvailableBalance = wallet.AvailableBalance - req.Amount
                                                                      , FrozenBalance = wallet.FrozenBalance       + req.Amount
                                                                    })
                  .ConfigureAwait(false) == null) {
            throw new NetAdminUnexpectedException(Ln.结果非预期);
        }

        var ret = await Rpo.InsertAsync(req with { OwnerDeptId = userDeptId, FrozenBalanceBefore = frozenBalanceBefore }).ConfigureAwait(false);
        return ret.Adapt<QueryWalletFrozenRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public async Task<QueryWalletFrozenRsp> EditAsync(EditWalletFrozenReq req)
    {
        req.ThrowIfInvalid();
        #if DBTYPE_SQLSERVER
        return (await UpdateReturnListAsync(req).ConfigureAwait(false)).FirstOrDefault()?.Adapt<QueryWalletFrozenRsp>();
        #else
        return await UpdateAsync(req).ConfigureAwait(false) > 0
            ? await GetAsync(new QueryWalletFrozenReq { Id = req.Id }).ConfigureAwait(false)
            : null;
        #endif
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryWalletFrozenReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QueryWalletFrozenReq, QueryWalletFrozenRsp>(QueryInternal, req, Ln.钱包冻结导出);
    }

    /// <inheritdoc />
    public async Task<QueryWalletFrozenRsp> GetAsync(QueryWalletFrozenReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryWalletFrozenReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryWalletFrozenRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryWalletFrozenRsp>> PagedQueryAsync(PagedQueryReq<QueryWalletFrozenReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Include(a => a.Owner)
                         .Page(req.Page, req.PageSize)
                         .WithNoLockNoWait()
                         .Count(out var total)
                         .ToListAsync(req)
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryWalletFrozenRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryWalletFrozenRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryWalletFrozenRsp>> QueryAsync(QueryReq<QueryWalletFrozenReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().Take(req.Count).ToListAsync(req).ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryWalletFrozenRsp>>();
    }

    /// <inheritdoc />
    public async Task<int> SetStatusToThawedAsync(SetStatusToThawedReq req)
    {
        req.ThrowIfInvalid();

        var frozen = await GetAsync(new QueryWalletFrozenReq { Id = req.Id }).ConfigureAwait(false);
        if (frozen?.Status != WalletFrozenStatues.Frozen) {
            throw new NetAdminInvalidOperationException(Ln.冻结状态不正确);
        }

        var updateCnt = await UpdateAsync(new Sys_WalletFrozen { Status = WalletFrozenStatues.Thawed }, [nameof(req.Status)], null
                                        , a => a.Status == WalletFrozenStatues.Frozen && a.Id == req.Id)
            .ConfigureAwait(false);
        if (updateCnt != 1) {
            throw new NetAdminUnexpectedException(Ln.结果非预期);
        }

        var userWalletService = S<IUserWalletService>();
        var wallet            = await userWalletService.GetAsync(new QueryUserWalletReq { Id = frozen.OwnerId!.Value }).ConfigureAwait(false);
        if (wallet.FrozenBalance < req.Amount) {
            throw new NetAdminUnexpectedException(Ln.结果非预期);
        }

        _ = await userWalletService
                  .EditAsync(wallet.Adapt<EditUserWalletReq>() with {
                                                                        AvailableBalance = wallet.AvailableBalance + frozen.Amount
                                                                      , FrozenBalance = wallet.FrozenBalance       - frozen.Amount
                                                                    })
                  .ConfigureAwait(false) ?? throw new NetAdminUnexpectedException(Ln.结果非预期);
        return 1;
    }

    private ISelect<Sys_WalletFrozen> QueryInternal(QueryReq<QueryWalletFrozenReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter);

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