using NetAdmin.Domain.Dto.Sys.DepositOrder;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IDepositOrderCache" />
public sealed class DepositOrderCache(IDistributedCache cache, IDepositOrderService service)
    : DistributedCache<IDepositOrderService>(cache, service), IScoped, IDepositOrderCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryDepositOrderReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryDepositOrderReq> req)
    {
        return Service.CountByAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDepositOrderRsp> CreateAsync(CreateDepositOrderReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDepositOrderRsp> EditAsync(EditDepositOrderReq req)
    {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryDepositOrderReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDepositOrderRsp> GetAsync(QueryDepositOrderReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<GetDepositConfigRsp> GetDepositConfigAsync()
    {
        return Service.GetDepositConfigAsync();
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDepositOrderRsp>> PagedQueryAsync(PagedQueryReq<QueryDepositOrderReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<int> PayConfirmAsync(PayConfirmReq req)
    {
        return Service.PayConfirmAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryDepositOrderRsp>> QueryAsync(QueryReq<QueryDepositOrderReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<int> ReceivedConfirmationAsync(JobReq req)
    {
        return Service.ReceivedConfirmationAsync(req);
    }
}