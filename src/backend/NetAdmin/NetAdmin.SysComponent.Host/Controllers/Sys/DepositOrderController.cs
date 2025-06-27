using NetAdmin.Domain.Dto.Sys.DepositOrder;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     充值订单服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class DepositOrderController(IDepositOrderCache cache)
    : ControllerBase<IDepositOrderCache, IDepositOrderService>(cache), IDepositOrderModule
{
    /// <summary>
    ///     批量删除充值订单
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     充值订单计数
    /// </summary>
    public Task<long> CountAsync(QueryReq<QueryDepositOrderReq> req)
    {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     充值订单分组计数
    /// </summary>
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryDepositOrderReq> req)
    {
        return Cache.CountByAsync(req);
    }

    /// <summary>
    ///     创建充值订单
    /// </summary>
    [Transaction]
    public Task<QueryDepositOrderRsp> CreateAsync(CreateDepositOrderReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除充值订单
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     编辑充值订单
    /// </summary>
    [Transaction]
    public Task<QueryDepositOrderRsp> EditAsync(EditDepositOrderReq req)
    {
        return Cache.EditAsync(req);
    }

    /// <summary>
    ///     导出充值订单
    /// </summary>
    [NonAction]
    public Task<IActionResult> ExportAsync(QueryReq<QueryDepositOrderReq> req)
    {
        return Cache.ExportAsync(req);
    }

    /// <summary>
    ///     获取单个充值订单
    /// </summary>
    public Task<QueryDepositOrderRsp> GetAsync(QueryDepositOrderReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     获取充值配置
    /// </summary>
    public Task<GetDepositConfigRsp> GetDepositConfigAsync()
    {
        return Cache.GetDepositConfigAsync();
    }

    /// <summary>
    ///     分页查询充值订单
    /// </summary>
    public Task<PagedQueryRsp<QueryDepositOrderRsp>> PagedQueryAsync(PagedQueryReq<QueryDepositOrderReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     支付确认
    /// </summary>
    public Task<int> PayConfirmAsync(PayConfirmReq req)
    {
        return Cache.PayConfirmAsync(req);
    }

    /// <summary>
    ///     查询充值订单
    /// </summary>
    [NonAction]
    public Task<IEnumerable<QueryDepositOrderRsp>> QueryAsync(QueryReq<QueryDepositOrderReq> req)
    {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     到账确认
    /// </summary>
    public Task<int> ReceivedConfirmationAsync(ReceivedConfirmationReq req)
    {
        return Cache.ReceivedConfirmationAsync(req);
    }
}