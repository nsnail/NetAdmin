using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.WalletTrade;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IWalletTradeCache" />
public sealed class WalletTradeCache(IDistributedCache cache, IWalletTradeService service)
    : DistributedCache<IWalletTradeService>(cache, service), IScoped, IWalletTradeCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req) {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryWalletTradeReq> req) {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryWalletTradeReq> req) {
        return Service.CountByAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryWalletTradeRsp> CreateAsync(CreateWalletTradeReq req) {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req) {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryWalletTradeRsp> EditAsync(EditWalletTradeReq req) {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryWalletTradeReq> req) {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryWalletTradeRsp> GetAsync(QueryWalletTradeReq req) {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<GetBarChartRsp>> GetSelfDepositBarChartAsync(QueryReq<QueryWalletTradeReq> req) {
        return Service.GetSelfDepositBarChartAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryWalletTradeRsp>> PagedQueryAsync(PagedQueryReq<QueryWalletTradeReq> req) {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryWalletTradeRsp>> QueryAsync(QueryReq<QueryWalletTradeReq> req) {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<decimal> SumAsync(QueryReq<QueryWalletTradeReq> req) {
        return Service.SumAsync(req);
    }

    /// <inheritdoc />
    public Task<long> TotalAmountAsync(QueryReq<QueryWalletTradeReq> req) {
        return Service.TotalAmountAsync(req);
    }

    /// <inheritdoc />
    public Task<int> TransferFromAnotherAccountAsync(TransferReq req) {
        return Service.TransferFromAnotherAccountAsync(req);
    }

    /// <inheritdoc />
    public Task<int> TransferToAnotherAccountAsync(TransferReq req) {
        return Service.TransferToAnotherAccountAsync(req);
    }
}