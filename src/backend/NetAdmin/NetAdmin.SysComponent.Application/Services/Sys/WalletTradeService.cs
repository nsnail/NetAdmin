using NetAdmin.Application.Extensions;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserInvite;
using NetAdmin.Domain.Dto.Sys.UserWallet;
using NetAdmin.Domain.Dto.Sys.WalletTrade;
using NetAdmin.Domain.Extensions;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IWalletTradeService" />
public sealed class WalletTradeService(BasicRepository<Sys_WalletTrade, long> rpo) //
    : RepositoryService<Sys_WalletTrade, long, IWalletTradeService>(rpo), IWalletTradeService
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
    public Task<long> CountAsync(QueryReq<QueryWalletTradeReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryWalletTradeReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        .WithNoLockNoWait()
                        .GroupBy(req.GetToListExp<Sys_WalletTrade>())
                        .ToDictionaryAsync(a => a.Count())
                        .ConfigureAwait(false);
        return ret.Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                              req.RequiredFields.ToImmutableDictionary(
                                  y => y, y => typeof(Sys_WalletTrade).GetProperty(y)!.GetValue(x.Key)?.ToString()), x.Value))
                  .Where(x => x.Key.Any(y => !y.Value.NullOrEmpty()))
                  .OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<QueryWalletTradeRsp> CreateAsync(CreateWalletTradeReq req)
    {
        req.ThrowIfInvalid();
        var userWalletService = S<IUserWalletService>();
        var wallet            = await userWalletService.GetAsync(new QueryUserWalletReq { Id = req.OwnerId!.Value }).ConfigureAwait(false);
        if (wallet.AvailableBalance + req.Amount < 0) {
            throw new NetAdminInvalidOperationException(Ln.钱包可用余额不足);
        }

        _ = await userWalletService.EditAsync(wallet.Adapt<EditUserWalletReq>() with { AvailableBalance = wallet.AvailableBalance + req.Amount })
                                   .ConfigureAwait(false) ?? throw new NetAdminUnexpectedException(Ln.交易失败);
        var ret = await Rpo.InsertAsync(req with { BalanceBefore = wallet.AvailableBalance, OwnerDeptId = wallet.OwnerDeptId }).ConfigureAwait(false);
        return ret.Adapt<QueryWalletTradeRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public async Task<QueryWalletTradeRsp> EditAsync(EditWalletTradeReq req)
    {
        req.ThrowIfInvalid();
        #if DBTYPE_SQLSERVER
        return (await UpdateReturnListAsync(req).ConfigureAwait(false)).FirstOrDefault()?.Adapt<QueryWalletTradeRsp>();
        #else
        return await UpdateAsync(req).ConfigureAwait(false) > 0
            ? await GetAsync(new QueryWalletTradeReq { Id = req.Id }).ConfigureAwait(false)
            : null;
        #endif
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryWalletTradeReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QueryWalletTradeReq, QueryWalletTradeRsp>(QueryInternal, req, Ln.钱包交易导出);
    }

    /// <inheritdoc />
    public async Task<QueryWalletTradeRsp> GetAsync(QueryWalletTradeReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryWalletTradeReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryWalletTradeRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryWalletTradeRsp>> PagedQueryAsync(PagedQueryReq<QueryWalletTradeReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Include(a => a.Owner.Invite.Owner)
                         .Page(req.Page, req.PageSize)
                         .WithNoLockNoWait()
                         .Count(out var total)
                         .ToListAsync(req)
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryWalletTradeRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryWalletTradeRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryWalletTradeRsp>> QueryAsync(QueryReq<QueryWalletTradeReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().Take(req.Count).ToListAsync(req).ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryWalletTradeRsp>>();
    }

    /// <inheritdoc />
    public async Task<decimal> SumAsync(QueryReq<QueryWalletTradeReq> req)
    {
        req.ThrowIfInvalid();
        return req.RequiredFields[0].Equals(nameof(QueryWalletTradeReq.Amount), StringComparison.OrdinalIgnoreCase)
            ? await QueryInternal(req with { Order = Orders.None })
                    .WithNoLockNoWait()
                    .SumAsync(a => SqlExt.Case().When(a.Amount < 0, -a.Amount).Else(a.Amount).End())
                    .ConfigureAwait(false)
            : await QueryInternal(req with { Order = Orders.None })
                    .WithNoLockNoWait()
                    .SumAsync(req.GetSumExp<Sys_WalletTrade>())
                    .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<int> TransferFromAnotherAccountAsync(TransferReq req)
    {
        // 检查源账户是不是自己的下级
        var fromAccount = await S<IUserInviteService>().GetAsync(new QueryUserInviteReq { Id = req.OwnerId!.Value }).ConfigureAwait(false);
        if (fromAccount == null || fromAccount.OwnerId != UserToken.Id) {
            throw new NetAdminInvalidOperationException(Ln.此操作不被允许);
        }

        // 不允许自己对自己转账
        if (UserToken.Id == fromAccount.Id) {
            throw new NetAdminInvalidOperationException(Ln.此操作不被允许);
        }

        var fromUser = await S<IUserService>().GetAsync(new QueryUserReq { Id = fromAccount.Id }).ConfigureAwait(false);

        // 源账户扣钱
        _ = await CreateAsync(new CreateWalletTradeReq {
                                                           OwnerDeptId    = fromUser.DeptId
                                                         , Amount         = -req.Amount
                                                         , OwnerId        = fromUser.Id
                                                         , Summary        = req.Summary
                                                         , TradeDirection = TradeDirections.Expense
                                                         , TradeType      = TradeTypes.TransferExpense
                                                       })
            .ConfigureAwait(false);

        // 自己账户加钱
        _ = await CreateAsync(new CreateWalletTradeReq {
                                                           Amount         = req.Amount
                                                         , Summary        = req.Summary
                                                         , TradeDirection = TradeDirections.Income
                                                         , TradeType      = TradeTypes.TransferIncome
                                                         , OwnerId        = UserToken.Id
                                                         , OwnerDeptId    = UserToken.DeptId
                                                       })
            .ConfigureAwait(false);
        return 1;
    }

    /// <inheritdoc />
    public async Task<int> TransferToAnotherAccountAsync(TransferReq req)
    {
        var toUser = await S<IUserService>().GetAsync(new QueryUserReq { Id = req.OwnerId!.Value }).ConfigureAwait(false);

        // 不允许自己对自己转账
        if (UserToken.Id == toUser.Id) {
            throw new NetAdminInvalidOperationException(Ln.此操作不被允许);
        }

        // 自己账户扣钱
        _ = await CreateAsync(new CreateWalletTradeReq {
                                                           Amount         = -req.Amount
                                                         , Summary        = req.Summary
                                                         , TradeDirection = TradeDirections.Expense
                                                         , TradeType      = TradeTypes.TransferExpense
                                                         , OwnerId        = UserToken.Id
                                                         , OwnerDeptId    = UserToken.DeptId
                                                       })
            .ConfigureAwait(false);

        // 他人账户加钱
        _ = await CreateAsync(new CreateWalletTradeReq {
                                                           OwnerDeptId    = toUser.DeptId
                                                         , Amount         = req.Amount
                                                         , OwnerId        = toUser.Id
                                                         , Summary        = req.Summary
                                                         , TradeDirection = TradeDirections.Income
                                                         , TradeType      = TradeTypes.TransferIncome
                                                       })
            .ConfigureAwait(false);
        return 1;
    }

    private ISelect<Sys_WalletTrade> QueryInternal(QueryReq<QueryWalletTradeReq> req)
    {
        return QueryInternal(req, null);
    }

    private ISelect<Sys_WalletTrade> QueryInternal(QueryReq<QueryWalletTradeReq> req, List<long> userIds)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereIf(req.Filter?.Id > 0, a => a.Id == req.Filter.Id)
                     .WhereIf(userIds?.Count > 0, a => userIds.Contains(a.OwnerId.Value));

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