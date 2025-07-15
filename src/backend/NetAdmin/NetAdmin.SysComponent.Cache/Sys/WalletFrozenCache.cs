using NetAdmin.Domain.Dto.Sys.WalletFrozen;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IWalletFrozenCache" />
public sealed class WalletFrozenCache(IDistributedCache cache, IWalletFrozenService service)
    : DistributedCache<IWalletFrozenService>(cache, service), IScoped, IWalletFrozenCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryWalletFrozenReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryWalletFrozenReq> req)
    {
        return Service.CountByAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryWalletFrozenRsp> CreateAsync(CreateWalletFrozenReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryWalletFrozenRsp> EditAsync(EditWalletFrozenReq req)
    {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryWalletFrozenReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryWalletFrozenRsp> GetAsync(QueryWalletFrozenReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryWalletFrozenRsp>> PagedQueryAsync(PagedQueryReq<QueryWalletFrozenReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryWalletFrozenRsp>> QueryAsync(QueryReq<QueryWalletFrozenReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<int> SetStatusToThawedAsync(SetStatusToThawedReq req)
    {
        return Service.SetStatusToThawedAsync(req);
    }
}