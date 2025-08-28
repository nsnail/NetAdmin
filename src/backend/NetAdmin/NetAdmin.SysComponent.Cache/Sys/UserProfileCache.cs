using NetAdmin.Domain.Dto.Sys.UserProfile;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IUserProfileCache" />
public sealed class UserProfileCache(IDistributedCache cache, IUserProfileService service)
    : DistributedCache<IUserProfileService>(cache, service), IScoped, IUserProfileCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req) {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryUserProfileReq> req) {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryUserProfileReq> req) {
        return Service.CountByAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserProfileRsp> CreateAsync(CreateUserProfileReq req) {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req) {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserProfileRsp> EditAsync(EditUserProfileReq req) {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryUserProfileReq> req) {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserProfileRsp> GetAsync(QueryUserProfileReq req) {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryUserProfileRsp>> PagedQueryAsync(PagedQueryReq<QueryUserProfileReq> req) {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryUserProfileRsp>> QueryAsync(QueryReq<QueryUserProfileReq> req) {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<decimal> SumAsync(QueryReq<QueryUserProfileReq> req) {
        return Service.SumAsync(req);
    }
}