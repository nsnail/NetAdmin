using NetAdmin.Domain.Dto.Sys.UserWallet;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IUserWalletCache" />
public sealed class UserWalletCache(IDistributedCache cache, IUserWalletService service)
    : DistributedCache<IUserWalletService>(cache, service), IScoped, IUserWalletCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req) {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryUserWalletReq> req) {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryUserWalletReq> req) {
        return Service.CountByAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserWalletRsp> CreateAsync(CreateUserWalletReq req) {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req) {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserWalletRsp> EditAsync(EditUserWalletReq req) {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryUserWalletReq> req) {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserWalletRsp> GetAsync(QueryUserWalletReq req) {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryUserWalletRsp>> PagedQueryAsync(PagedQueryReq<QueryUserWalletReq> req) {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryUserWalletRsp>> QueryAsync(QueryReq<QueryUserWalletReq> req) {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<decimal> SumAsync(QueryReq<QueryUserWalletReq> req) {
        return Service.SumAsync(req);
    }
}