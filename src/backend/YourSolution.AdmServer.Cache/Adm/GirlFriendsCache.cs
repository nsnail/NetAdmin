using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using YourSolution.AdmServer.Application.Services.Adm.Dependency;
using YourSolution.AdmServer.Cache.Adm.Dependency;
using YourSolution.AdmServer.Domain.Dto.Adm.GirlFriends;

namespace YourSolution.AdmServer.Cache.Adm;

/// <inheritdoc cref="IGirlFriendsCache" />
public sealed class GirlFriendsCache(IDistributedCache cache, IGirlFriendsService service)
    : DistributedCache<IGirlFriendsService>(cache, service), IScoped, IGirlFriendsCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryGirlFriendsReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryGirlFriendsReq> req)
    {
        return Service.CountByAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryGirlFriendsRsp> CreateAsync(CreateGirlFriendsReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryGirlFriendsRsp> EditAsync(EditGirlFriendsReq req)
    {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryGirlFriendsReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryGirlFriendsRsp> GetAsync(QueryGirlFriendsReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryGirlFriendsRsp>> PagedQueryAsync(PagedQueryReq<QueryGirlFriendsReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryGirlFriendsRsp>> QueryAsync(QueryReq<QueryGirlFriendsReq> req)
    {
        return Service.QueryAsync(req);
    }
}