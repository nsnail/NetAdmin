using NetAdmin.Application.Extensions;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.DepositOrder;
using NetAdmin.Domain.Dto.Sys.WalletTrade;
using NetAdmin.Domain.Extensions;
using NetAdmin.Infrastructure.Rpc.TronScan;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDepositOrderService" />
public sealed class DepositOrderService(BasicRepository<Sys_DepositOrder, long> rpo) //
    : RepositoryService<Sys_DepositOrder, long, IDepositOrderService>(rpo), IDepositOrderService
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
    public Task<long> CountAsync(QueryReq<QueryDepositOrderReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryDepositOrderReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        .WithNoLockNoWait()
                        .GroupBy(req.GetToListExp<Sys_DepositOrder>())
                        .ToDictionaryAsync(a => a.Count())
                        .ConfigureAwait(false);
        return ret.Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                              req.RequiredFields.ToImmutableDictionary(
                                  y => y, y => typeof(Sys_DepositOrder).GetProperty(y)!.GetValue(x.Key)?.ToString()), x.Value))
                  .Where(x => x.Key.Any(y => !y.Value.NullOrEmpty()))
                  .OrderByDescending(x => x.Value);
    }

    /// <exception cref="ArgumentOutOfRangeException">req</exception>
    /// <inheritdoc />
    public async Task<QueryDepositOrderRsp> CreateAsync(CreateDepositOrderReq req)
    {
        req.ThrowIfInvalid();

        var config = await GetDepositConfigAsync().ConfigureAwait(false);

        var toPointRate = req.PaymentMode switch {
                              PaymentModes.USDT   => config.UsdToPointRate
                            , PaymentModes.Alipay => config.CnyToPointRate
                            , PaymentModes.WeChat => config.CnyToPointRate
                            , _                   => throw new ArgumentOutOfRangeException(nameof(req))
                          };

        var ret = await Rpo.InsertAsync(req with {
                                                     ActualPayAmount = (long)(((decimal)req.DepositPoint / toPointRate).Round(3) * 1000)
                                                   , ToPointRate = toPointRate
                                                   , ReceiptAccount = config.Trc20ReceiptAddress
                                                 })
                           .ConfigureAwait(false);
        return ret.Adapt<QueryDepositOrderRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public async Task<QueryDepositOrderRsp> EditAsync(EditDepositOrderReq req)
    {
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
    public Task<IActionResult> ExportAsync(QueryReq<QueryDepositOrderReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QueryDepositOrderReq, QueryDepositOrderRsp>(QueryInternal, req, Ln.充值订单导出);
    }

    /// <inheritdoc />
    public async Task<QueryDepositOrderRsp> GetAsync(QueryDepositOrderReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryDepositOrderReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryDepositOrderRsp>();
    }

    /// <inheritdoc />
    public async Task<GetDepositConfigRsp> GetDepositConfigAsync()
    {
        var ret = await S<IConfigService>().GetLatestConfigAsync().ConfigureAwait(false);
        return ret.Adapt<GetDepositConfigRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryDepositOrderRsp>> PagedQueryAsync(PagedQueryReq<QueryDepositOrderReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Include(a => a.Owner)
                         .Page(req.Page, req.PageSize)
                         .WithNoLockNoWait()
                         .Count(out var total)
                         .ToListAsync(req)
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryDepositOrderRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryDepositOrderRsp>>());
    }

    /// <inheritdoc />
    public Task<int> PayConfirmAsync(PayConfirmReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req with { DepositOrderStatus = DepositOrderStatues.PaymentConfirming }, [nameof(req.DepositOrderStatus)], null
                         , a => a.DepositOrderStatus == DepositOrderStatues.WaitingForPayment && a.Id == req.Id, null, true);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryDepositOrderRsp>> QueryAsync(QueryReq<QueryDepositOrderReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().Take(req.Count).ToListAsync(req).ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryDepositOrderRsp>>();
    }

    /// <inheritdoc />
    public async Task<int> ReceivedConfirmationAsync(ReceivedConfirmationReq req)
    {
        req.ThrowIfInvalid();
        var ret    = 0;
        var config = await S<IConfigService>().GetLatestConfigAsync().ConfigureAwait(false);
        var waitConfirmList = (await QueryAsync(new QueryReq<QueryDepositOrderReq> {
                                                                                       Count = Numbers.MAX_LIMIT_QUERY_PAGE_SIZE
                                                                                     , DynamicFilter
                                                                                           = new DynamicFilterInfo {
                                                                                                 Field = nameof(
                                                                                                     QueryDepositOrderReq.DepositOrderStatus)
                                                                                               , Operator = DynamicFilterOperators.Eq
                                                                                               , Value    = DepositOrderStatues.PaymentConfirming
                                                                                             }
                                                                                     , Order = Orders.Ascending
                                                                                     , Prop  = nameof(QueryDepositOrderReq.Id)
                                                                                   })
            .ConfigureAwait(false)).ToList();
        var apiResult = await S<ITronScanClient>()
                              .TransfersAsync(S<IOptions<TronScanOptions>>().Value.Token, req.ReadRecordCount, config.Trc20ReceiptAddress)
                              .ConfigureAwait(false);
        foreach (var apiItem in apiResult.TokenTransfers.Where(x => x.TokenInfo.TokenAbbr == "USDT" && x.Confirmed && x.ContractRet == "SUCCESS" &&
                                                                    x.FinalResult         == "SUCCESS")) {
            var order = waitConfirmList.SingleOrDefault(x => x.ActualPayAmount == apiItem.Quant.Int64() / 1000);
            if (order == null || order.CreatedTime > apiItem.BlockTs.Time()) {
                continue;
            }

            try {
                await S<UnitOfWorkManager>()
                      .AtomicOperateAsync(async () => {
                          var updated = await UpdateAsync( //
                                  new Sys_DepositOrder {
                                                           DepositOrderStatus = DepositOrderStatues.Succeeded
                                                         , Version = order.Version
                                                         , FinishTimestamp = DateTime.Now.TimeUnixUtcMs()
                                                         , PaidAccount = apiItem.FromAddress
                                                         , PaidTime = apiItem.BlockTs.Time()
                                                         , PaymentFinger = apiItem.TransactionId
                                                       }, [nameof(Sys_DepositOrder.DepositOrderStatus), nameof(Sys_DepositOrder.FinishTimestamp), nameof(Sys_DepositOrder.PaidAccount), nameof(Sys_DepositOrder.PaidTime), nameof(Sys_DepositOrder.PaymentFinger)]
                                , null, a => a.Id == order.Id && a.DepositOrderStatus == DepositOrderStatues.PaymentConfirming)
                              .ConfigureAwait(false);
                          if (updated != 1) {
                              throw new NetAdminUnexpectedException(Ln.结果非预期);
                          }

                          _ = await S<IWalletTradeService>()
                                    .CreateAsync(new CreateWalletTradeReq {
                                                                              Amount              = order.DepositPoint
                                                                            , BusinessOrderNumber = order.Id
                                                                            , TradeDirection      = TradeDirections.Income
                                                                            , TradeType           = TradeTypes.SelfDeposit
                                                                            , OwnerId             = order.OwnerId
                                                                          })
                                    .ConfigureAwait(false) ?? throw new NetAdminUnexpectedException(Ln.结果非预期);
                      })
                      .ConfigureAwait(false);
                ret++;
            }
            catch {
                // ignore
            }
        }

        return ret;
    }

    private ISelect<Sys_DepositOrder> QueryInternal(QueryReq<QueryDepositOrderReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter);

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (req.Order) {
            case Orders.None:
                return ret;
            case Orders.Random:
                return ret.OrderByRandom();
        }

        ret = ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);
        if (!req.Prop?.Equals(nameof(req.Filter.Id), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Id);
        }

        return ret;
    }
}