using System.Net.Http.Headers;
using Ganss.Excel;
using NetAdmin.Application.Extensions;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserInvite;
using NetAdmin.Domain.Dto.Sys.UserWallet;
using NetAdmin.Domain.Dto.Sys.WalletTrade;
using NetAdmin.Domain.Extensions;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IWalletTradeService" />
public sealed class WalletTradeService(BasicRepository<Sys_WalletTrade, long> rpo)
    : RepositoryService<Sys_WalletTrade, long, IWalletTradeService>(rpo), IWalletTradeService
{
    private readonly Expression<Func<Sys_WalletTrade, Sys_WalletTrade>> _toListExp = a => new Sys_WalletTrade
    {
        Id = a.Id
        , CreatedTime = a.CreatedTime
        , Owner = new Sys_User
        {
            Id = a.Owner.Id
            , UserName = a.Owner.UserName
            , Avatar = a.Owner.Avatar
            , Invite = new Sys_UserInvite
            {
                Owner = new Sys_User
                {
                    Id = a.Owner.Invite.Owner.Id, UserName = a.Owner.Invite.Owner.UserName, Avatar = a.Owner.Invite.Owner.Avatar
                }
                , Channel
                    = new Sys_User
                    {
                        Id = a.Owner.Invite.Channel.Id
                        , UserName = a.Owner.Invite.Channel.UserName
                        , Avatar = a.Owner.Invite.Channel.Avatar
                    }
            }
        }
        , Summary = a.Summary
        , TradeDirection = a.TradeDirection
        , TradeType = a.TradeType
        , BusinessOrderNumber = a.BusinessOrderNumber
        , Amount = a.Amount
        , BalanceBefore = a.BalanceBefore
        , CreatedUserId = a.CreatedUserId
        , CreatedUserName = a.CreatedUserName
    };

    /// <inheritdoc />
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req) {
        req.ThrowIfInvalid();
        var ret = 0;

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var item in req.Items) {
            ret += await DeleteAsync(item).ConfigureAwait(false);
        }

        return ret;
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryWalletTradeReq> req) {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryWalletTradeReq> req) {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
            .WithNoLockNoWait()
            .GroupBy(req.GetToListExp<Sys_WalletTrade>())
            .ToDictionaryAsync(a => a.Count())
            .ConfigureAwait(false);
        return ret
            .Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                    req.RequiredFields.ToImmutableDictionary(y => y, y => typeof(Sys_WalletTrade).GetProperty(y)!.GetValue(x.Key)?.ToString())
                    , x.Value
                )
            )
            .Where(x => x.Key.Any(y => !y.Value.NullOrEmpty()))
            .OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<QueryWalletTradeRsp> CreateAsync(CreateWalletTradeReq req) {
        req.ThrowIfInvalid();
        var userWalletService = S<IUserWalletService>();
        var wallet = await userWalletService.GetAsync(new QueryUserWalletReq { Id = req.OwnerId!.Value }).ConfigureAwait(false);
        if (wallet.AvailableBalance + req.Amount < 0) {
            throw new NetAdminInvalidOperationException(Ln.钱包可用余额不足);
        }

        _ = await userWalletService
                .EditAsync(wallet.Adapt<EditUserWalletReq>() with { AvailableBalance = wallet.AvailableBalance + req.Amount })
                .ConfigureAwait(false)
            ?? throw new NetAdminUnexpectedException(Ln.交易失败);
        var ret = await Rpo
            .InsertAsync(req with { BalanceBefore = wallet.AvailableBalance + wallet.FrozenBalance, OwnerDeptId = wallet.Owner.DeptId })
            .ConfigureAwait(false);
        return ret.Adapt<QueryWalletTradeRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req) {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public async Task<QueryWalletTradeRsp> EditAsync(EditWalletTradeReq req) {
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
    public async Task<IActionResult> ExportAsync(QueryReq<QueryWalletTradeReq> req) {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
            .Include(a => a.Owner.Invite.Owner)
            .Include(a => a.Owner.Invite.Channel)
            .WithNoLockNoWait()
            .Take(Numbers.MAX_LIMIT_EXPORT)
            .ToListAsync(_toListExp)
            .ConfigureAwait(false);

        var stream = new MemoryStream();
        var exportRsp = list.ConvertAll(x => new ExportWalletTradeRsp
            {
                备注 = x.Summary
                , 操作人 = x.CreatedUserName
                , 归属用户 = x.Owner.UserName
                , 交易编号 = x.Id.ToInvString()
                , 交易方向 = x.TradeDirection.ResDesc<Ln>()
                , 交易后余额 = ((x.BalanceBefore + x.Amount) / 100m).Round(2)
                , 交易金额 = (x.Amount / 100m).Round(2)
                , 交易类型 = x.TradeType.ResDesc<Ln>()
                , 交易前余额 = (x.BalanceBefore / 100m).Round(2)
                , 上级 = x.Owner.Invite.Owner?.UserName
                , 渠道 = x.Owner.Invite.Channel?.UserName
                , 业务订单号 = x.BusinessOrderNumber?.ToInvString()
            }
        );

        await new ExcelMapper().SaveAsync(stream, exportRsp).ConfigureAwait(false);

        _ = stream.Seek(0, SeekOrigin.Begin);
        App.HttpContext.Response.Headers.ContentDisposition = new ContentDispositionHeaderValue(Chars.FLG_HTTP_HEADER_VALUE_ATTACHMENT)
        {
            FileNameStar = $"交易流水{list.Count}个-{list.Min(x => x.CreatedTime):MMddHHmm}-{list.Max(x => x.CreatedTime):MMddHHmm}.xlsx"
        }.ToString();
        return new FileStreamResult(stream, Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_OCTET_STREAM);
    }

    /// <inheritdoc />
    public async Task<QueryWalletTradeRsp> GetAsync(QueryWalletTradeReq req) {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryWalletTradeReq> { Filter = req, Order = Orders.None })
            .Include(a => a.Owner.Invite.Owner)
            .Include(a => a.Owner.Invite.Channel)
            .ToOneAsync()
            .ConfigureAwait(false);
        return ret.Adapt<QueryWalletTradeRsp>();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<GetBarChartRsp>> GetSelfDepositBarChartAsync(QueryReq<QueryWalletTradeReq> req) {
        req.ThrowIfInvalid();
        var df = new DynamicFilterInfo
        {
            Field = nameof(Sys_WalletTrade.TradeType), Operator = DynamicFilterOperators.Eq, Value = TradeTypes.SelfDeposit
        };
        var newdf = req.DynamicFilter.Add(df);
        var ret = await QueryInternal(req with { Order = Orders.None, DynamicFilter = newdf })
            .WithNoLockNoWait()
            .GroupBy(a => new { a.CreatedTime.Year, a.CreatedTime.Month, a.CreatedTime.Day, a.CreatedTime.Hour })
            .ToListAsync(a => new GetBarChartRsp
                {
                    Timestamp = new DateTime(a.Key.Year, a.Key.Month, a.Key.Day, a.Key.Hour, 0, 0, DateTimeKind.Unspecified)
                    , Value = (int)Math.Floor(a.Sum(a.Value.Amount) / 100)
                }
            )
            .ConfigureAwait(false);
        return ret.OrderBy(x => x.Timestamp);
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryWalletTradeRsp>> PagedQueryAsync(PagedQueryReq<QueryWalletTradeReq> req) {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
            .Include(a => a.Owner.Invite.Owner)
            .Include(a => a.Owner.Invite.Channel)
            .Page(req.Page, req.PageSize)
            .WithNoLockNoWait()
            .Count(out var total)
            .ToListAsync(_toListExp)
            .ConfigureAwait(false);

        return new PagedQueryRsp<QueryWalletTradeRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryWalletTradeRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryWalletTradeRsp>> QueryAsync(QueryReq<QueryWalletTradeReq> req) {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().Take(req.Count).ToListAsync(req).ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryWalletTradeRsp>>();
    }

    /// <inheritdoc />
    public async Task<decimal> SumAsync(QueryReq<QueryWalletTradeReq> req) {
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
    public Task<long> TotalAmountAsync(QueryReq<QueryWalletTradeReq> req) {
        req.ThrowIfInvalid();
        var total = QueryInternal(req with { Order = Orders.None }).WithNoLockNoWait().Sum(a => a.Amount) / 100;
        return Task.FromResult((long)Math.Abs(Math.Floor(total)));
    }

    /// <inheritdoc />
    public async Task<int> TransferFromAnotherAccountAsync(TransferReq req) {
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
        _ = await CreateAsync(
                new CreateWalletTradeReq
                {
                    OwnerDeptId = fromUser.DeptId
                    , Amount = -req.Amount
                    , OwnerId = fromUser.Id
                    , Summary = $"{req.Summary} ({Ln.转给}: {UserToken.UserName}/{UserToken.Id})"
                    , TradeDirection = TradeDirections.Expense
                    , TradeType = TradeTypes.TransferExpense
                }
            )
            .ConfigureAwait(false);

        // 自己账户加钱
        _ = await CreateAsync(
                new CreateWalletTradeReq
                {
                    Amount = req.Amount
                    , Summary = $"{req.Summary} ({Ln.来自}: {fromUser.UserName}/{fromUser.Id})"
                    , TradeDirection = TradeDirections.Income
                    , TradeType = TradeTypes.TransferIncome
                    , OwnerId = UserToken.Id
                    , OwnerDeptId = UserToken.DeptId
                }
            )
            .ConfigureAwait(false);
        return 1;
    }

    /// <inheritdoc />
    public async Task<int> TransferToAnotherAccountAsync(TransferReq req) {
        var toUser = await S<IUserService>().GetAsync(new QueryUserReq { Id = req.OwnerId!.Value }).ConfigureAwait(false);

        // 不允许自己对自己转账
        if (UserToken.Id == toUser.Id) {
            throw new NetAdminInvalidOperationException(Ln.此操作不被允许);
        }

        // 自己账户扣钱
        _ = await CreateAsync(
                new CreateWalletTradeReq
                {
                    Amount = -req.Amount
                    , Summary = $"{req.Summary} ({Ln.转给}: {toUser.UserName}/{toUser.Id})"
                    , TradeDirection = TradeDirections.Expense
                    , TradeType = TradeTypes.TransferExpense
                    , OwnerId = UserToken.Id
                    , OwnerDeptId = UserToken.DeptId
                }
            )
            .ConfigureAwait(false);

        // 他人账户加钱
        _ = await CreateAsync(
                new CreateWalletTradeReq
                {
                    OwnerDeptId = toUser.DeptId
                    , Amount = req.Amount
                    , OwnerId = toUser.Id
                    , Summary = $"{req.Summary} ({Ln.来自}: {UserToken.UserName}/{UserToken.Id})"
                    , TradeDirection = TradeDirections.Income
                    , TradeType = TradeTypes.TransferIncome
                }
            )
            .ConfigureAwait(false);
        return 1;
    }

    private ISelect<Sys_WalletTrade> QueryInternal(QueryReq<QueryWalletTradeReq> req) {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereIf(req.Filter?.Id > 0, a => a.Id == req.Filter.Id);

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