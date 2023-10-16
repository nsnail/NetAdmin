using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.UserProfile;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IUserProfileCache" />
public sealed class UserProfileCache
    (IDistributedCache cache, IUserProfileService service) : DistributedCache<IUserProfileService>(cache, service)
                                                           , IScoped
                                                           , IUserProfileCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserProfileRsp> CreateAsync(CreateUserProfileReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryUserProfileReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserProfileRsp> GetAsync(QueryUserProfileReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryUserProfileRsp>> PagedQueryAsync(PagedQueryReq<QueryUserProfileReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryUserProfileRsp>> QueryAsync(QueryReq<QueryUserProfileReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserProfileRsp> UpdateAsync(UpdateUserProfileReq req)
    {
        return Service.UpdateAsync(req);
    }
}