using NetAdmin.Domain.Dto.Sys.UserInvite;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IUserInviteCache" />
public sealed class UserInviteCache(IDistributedCache cache, IUserInviteService service)
    : DistributedCache<IUserInviteService>(cache, service), IScoped, IUserInviteCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryUserInviteReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryUserInviteReq> req)
    {
        return Service.CountByAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserInviteRsp> CreateAsync(CreateUserInviteReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserInviteRsp> EditAsync(EditUserInviteReq req)
    {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryUserInviteReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserInviteRsp> GetAsync(QueryUserInviteReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryUserInviteRsp>> PagedQueryAsync(PagedQueryReq<QueryUserInviteReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryUserInviteRsp>> QueryAsync(QueryReq<QueryUserInviteReq> req)
    {
        return Service.QueryAsync(req);
    }
}