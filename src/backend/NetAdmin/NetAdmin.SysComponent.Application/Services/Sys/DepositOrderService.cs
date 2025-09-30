using System.Net.Http.Headers;
using Ganss.Excel;
using NetAdmin.Application.Extensions;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.DepositOrder;
using NetAdmin.Domain.Dto.Sys.WalletTrade;
using NetAdmin.Domain.Enums.Sys;
using NetAdmin.Domain.Extensions;
using NetAdmin.Infrastructure.Rpc.TronScan;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDepositOrderService" />
public sealed class DepositOrderService(BasicRepository<Sys_DepositOrder, long> rpo)
    : RepositoryService<Sys_DepositOrder, long, IDepositOrderService>(rpo), IDepositOrderService
{
    private readonly Expression<Func<Sys_DepositOrder, Sys_DepositOrder>> _toListExp = a => new Sys_DepositOrder
    {
        Id = a.Id
        , CreatedTime = a.CreatedTime
        , ModifiedTime = a.ModifiedTime
        , Version = a.Version
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
        , DepositOrderStatus = a.DepositOrderStatus
        , ActualPayAmount = a.ActualPayAmount
        , DepositPoint = a.DepositPoint
        , ToPointRate = a.ToPointRate
        , PaymentMode = a.PaymentMode
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
    public Task<long> CountAsync(QueryReq<QueryDepositOrderReq> req) {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryDepositOrderReq> req) {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
            .WithNoLockNoWait()
            .GroupBy(req.GetToListExp<Sys_DepositOrder>())
            .ToDictionaryAsync(a => a.Count())
            .ConfigureAwait(false);
        return ret
            .Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                    req.RequiredFields.ToImmutableDictionary(y => y, y => typeof(Sys_DepositOrder).GetProperty(y)!.GetValue(x.Key)?.ToString())
                    , x.Value
                )
            )
            .Where(x => x.Key.Any(y => !y.Value.NullOrEmpty()))
            .OrderByDescending(x => x.Value);
    }

    /// <exception cref="ArgumentOutOfRangeException">req</exception>
    /// <inheritdoc />
    public async Task<QueryDepositOrderRsp> CreateAsync(CreateDepositOrderReq req) {
        req.ThrowIfInvalid();

        var config = await GetDepositConfigAsync().ConfigureAwait(false);

        var toPointRate = req.PaymentMode switch
        {
            PaymentModes.USDT => config.UsdToPointRate
            , PaymentModes.Alipay or PaymentModes.WeChat => config.CnyToPointRate
            , _ => throw new ArgumentOutOfRangeException(nameof(req))
        };

        var ret = await Rpo
            .InsertAsync(
                req with
                {
                    ActualPayAmount = (long)(((decimal)req.DepositPoint / toPointRate).Round(3) * 1000)
                    , ToPointRate = toPointRate
                    , ReceiptAccount = config.Trc20ReceiptAddress
                }
            )
            .ConfigureAwait(false);
        return ret.Adapt<QueryDepositOrderRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req) {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a =>
            a.Id == req.Id
            && (a.DepositOrderStatus == DepositOrderStatues.WaitingForPayment || a.DepositOrderStatus == DepositOrderStatues.PaymentConfirming)
        );
    }

    /// <inheritdoc />
    public async Task<QueryDepositOrderRsp> EditAsync(EditDepositOrderReq req) {
        req.ThrowIfInvalid();
        #if DBTYPE_SQLSERVER
        return (await UpdateReturnListAsync(req).ConfigureAwait(false)).FirstOrDefault()?.Adapt<QueryDepositOrderRsp>();
        #else
        return await UpdateAsync(req).ConfigureAwait(false) > 0
            ? await GetAsync(new QueryDepositOrderReq { Id = req.Id }).ConfigureAwait(false)
            : null;
        #endif
    }

    /// <inheritdoc />
    public async Task<IActionResult> ExportAsync(QueryReq<QueryDepositOrderReq> req) {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
            .Include(a => a.Owner)
            .WithNoLockNoWait()
            .Take(Numbers.MAX_LIMIT_EXPORT)
            .ToListAsync(_toListExp)
            .ConfigureAwait(false);

        var stream = new MemoryStream();
        await new ExcelMapper()
            .SaveAsync(
                stream
                , list.ConvertAll(x => new ExportDepositOrderRsp
                    {
                        充值金额 = (x.DepositPoint / 100m).Round(2)
                        , 订单编号 = x.Id.ToInvString()
                        , 订单状态 = x.DepositOrderStatus.ResDesc<Ln>()
                        , 归属用户 = x.Owner.UserName
                        , 上级 = x.Owner.Invite.Owner?.UserName
                        , 渠道 = x.Owner.Invite.Channel?.UserName
                        , 汇率 = (x.ToPointRate / 100m).Round(2)
                        , 支付方式 = x.PaymentMode.ResDesc<Ln>()
                        , 支付金额 = (x.ActualPayAmount / 1000m).Round(3)
                    }
                )
            )
            .ConfigureAwait(false);
        _ = stream.Seek(0, SeekOrigin.Begin);
        App.HttpContext.Response.Headers.ContentDisposition = new ContentDispositionHeaderValue(Chars.FLG_HTTP_HEADER_VALUE_ATTACHMENT)
        {
            FileNameStar = $"充值订单{list.Count}个-{list.Min(x => x.CreatedTime):MMddHHmm}-{list.Max(x => x.CreatedTime):MMddHHmm}.xlsx"
        }.ToString();
        return new FileStreamResult(stream, Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_OCTET_STREAM);
    }

    /// <inheritdoc />
    public async Task<QueryDepositOrderRsp> GetAsync(QueryDepositOrderReq req) {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryDepositOrderReq> { Filter = req, Order = Orders.None })
            .Include(a => a.Owner.Invite.Owner)
            .Include(a => a.Owner.Invite.Channel)
            .ToOneAsync()
            .ConfigureAwait(false);
        return ret.Adapt<QueryDepositOrderRsp>();
    }

    /// <inheritdoc />
    public async Task<GetDepositConfigRsp> GetDepositConfigAsync() {
        var ret = await S<IConfigService>().GetLatestConfigAsync().ConfigureAwait(false);
        return ret.Adapt<GetDepositConfigRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryDepositOrderRsp>> PagedQueryAsync(PagedQueryReq<QueryDepositOrderReq> req) {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
            .Include(a => a.Owner.Invite.Owner)
            .Include(a => a.Owner.Invite.Channel)
            .Page(req.Page, req.PageSize)
            .WithNoLockNoWait()
            .Count(out var total)
            .ToListAsync(_toListExp)
            .ConfigureAwait(false);

        return new PagedQueryRsp<QueryDepositOrderRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryDepositOrderRsp>>());
    }

    /// <inheritdoc />
    public Task<int> PayConfirmAsync(PayConfirmReq req) {
        req.ThrowIfInvalid();
        return UpdateAsync(
            req with { DepositOrderStatus = DepositOrderStatues.PaymentConfirming }, [nameof(req.DepositOrderStatus)], null
            , a => a.DepositOrderStatus == DepositOrderStatues.WaitingForPayment && a.Id == req.Id, null, true
        );
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryDepositOrderRsp>> QueryAsync(QueryReq<QueryDepositOrderReq> req) {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().Take(req.Count).ToListAsync(req).ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryDepositOrderRsp>>();
    }

    /// <inheritdoc />
    public async Task<int> ReceivedConfirmationAsync(JobReq req) {
        req.ThrowIfInvalid();

        // 初始化返回值
        var ret = 0;

        // 获取最新配置信息
        var config = await S<IConfigService>().GetLatestConfigAsync().ConfigureAwait(false);

        // 查询所有状态为“待确认”的充值订单
        var waitConfirmList = (await QueryAsync(
                new QueryReq<QueryDepositOrderReq>
                {
                    Count = Numbers.MAX_LIMIT_QUERY_PAGE_SIZE
                    , DynamicFilter
                        = new DynamicFilterInfo
                        {
                            Field = nameof(QueryDepositOrderReq.DepositOrderStatus)
                            , Operator = DynamicFilterOperators.Eq
                            , Value = DepositOrderStatues.PaymentConfirming
                        }
                    , Order = Orders.Ascending
                    , Prop = nameof(QueryDepositOrderReq.Id)
                }
            )
            .ConfigureAwait(false)).ToList();

        // 调用 TronScan API 获取指定地址的代币转账记录
        var apiResult = await S<ITronScanClient>()
            .TransfersAsync(S<IOptions<TronScanOptions>>().Value.Token, req.Count!.Value, config.Trc20ReceiptAddress)
            .ConfigureAwait(false);

        // 如果没有获取到交易数据，则直接返回 0
        if (apiResult.TokenTransfers == null) {
            return ret;
        }

        // 遍历所有已确认且成功的 USDT 交易
        foreach (var apiItem in apiResult.TokenTransfers.Where(x =>
                     x.TokenInfo.TokenAbbr == "USDT" && x.Confirmed && x.ContractRet == "SUCCESS" && x.FinalResult == "SUCCESS"
                 )) {
            // 匹配本地订单中金额一致、创建时间早于交易时间的订单
            var order = waitConfirmList.SingleOrDefault(x => x.ActualPayAmount == apiItem.Quant.Int64() / 1000);
            if (order == null || order.CreatedTime > apiItem.BlockTs.Time()) {
                continue;
            }

            try {
                // 使用工作单元模式原子性地更新订单状态并创建钱包交易记录
                await S<UnitOfWorkManager>()
                    .AtomicOperateAsync(async () => await OrderSuccessAsync(order, apiItem).ConfigureAwait(false))
                    .ConfigureAwait(false);
                ret++;
            }
            catch {
                // 忽略异常，防止中断整个流程
            }
        }

        return ret;
    }

    /// <inheritdoc />
    public Task<decimal> SumAsync(QueryReq<QueryDepositOrderReq> req) {
        req.ThrowIfInvalid();
        return QueryInternal(req with { Order = Orders.None }).WithNoLockNoWait().SumAsync(req.GetSumExp<Sys_DepositOrder>());
    }

    /// <summary>
    ///     将指定订单标记为成功，并创建对应的钱包交易记录。
    /// </summary>
    /// <param name="order">本地待处理的订单信息。</param>
    /// <param name="apiItem">来自链上的交易信息。</param>
    /// <exception cref="NetAdminUnexpectedException">当发生未预期的网络管理异常时抛出此异常</exception>
    private async Task OrderSuccessAsync(
        QueryDepositOrderRsp order
        , TokenTransferInfo apiItem
    ) {
        // 更新订单状态为成功，并填充支付相关信息
        var updated = await UpdateAsync(
                new Sys_DepositOrder
                {
                    DepositOrderStatus = DepositOrderStatues.Succeeded
                    , Version = order.Version
                    , FinishTimestamp = DateTime.Now.TimeUnixUtcMs()
                    , PaidAccount = apiItem.FromAddress
                    , PaidTime = apiItem.BlockTs.Time()
                    , PaymentFinger = apiItem.TransactionId
                }, [nameof(Sys_DepositOrder.DepositOrderStatus), nameof(Sys_DepositOrder.FinishTimestamp), nameof(Sys_DepositOrder.PaidAccount), nameof(Sys_DepositOrder.PaidTime), nameof(Sys_DepositOrder.PaymentFinger)]
                , null, a => a.Id == order.Id && a.DepositOrderStatus == DepositOrderStatues.PaymentConfirming
            )
            .ConfigureAwait(false);

        // 如果更新失败（影响行数不为1），抛出异常
        if (updated != 1) {
            throw new NetAdminUnexpectedException(Ln.结果非预期);
        }

        // 创建钱包交易记录
        _ = await S<IWalletTradeService>()
                .CreateAsync(
                    new CreateWalletTradeReq
                    {
                        Amount = order.DepositPoint
                        , BusinessOrderNumber = order.Id
                        , TradeDirection = TradeDirections.Income
                        , TradeType = TradeTypes.SelfDeposit
                        , OwnerId = order.OwnerId
                        , OwnerDeptId = order.OwnerDeptId
                    }
                )
                .ConfigureAwait(false)
            ?? throw new NetAdminUnexpectedException(Ln.结果非预期);
    }

    private ISelect<Sys_DepositOrder> QueryInternal(QueryReq<QueryDepositOrderReq> req) {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereIf(req.Filter?.Id > 0, a => a.Id == req.Filter.Id);

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (req.Order) {
            case Orders.None:
                return ret.AppendOtherFilters(req);
            case Orders.Random:
                return ret.OrderByRandom();
        }

        ret = ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);
        if (!req.Prop?.Equals(nameof(req.Filter.Id), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Id);
        }

        return ret.AppendOtherFilters(req);
    }
}