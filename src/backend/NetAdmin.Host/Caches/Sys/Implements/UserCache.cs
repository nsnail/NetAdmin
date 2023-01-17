using Furion.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.DataContract.Dto.Dependency;
using NetAdmin.DataContract.Dto.Sys.User;

#pragma warning disable CS1591

namespace NetAdmin.Host.Caches.Sys.Implements;

/// <inheritdoc cref="IUserCache" />
public class UserCache : CacheBase<IUserService>, IScoped, IUserCache
{
    private readonly TimeSpan _absoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
    private readonly TimeSpan _slidingExpiration               = TimeSpan.FromMinutes(5);

    public UserCache(IMemoryCache cache, IUserService service) //
        : base(cache, service) { }

    public Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        throw new NotImplementedException();
    }

    public Task<QueryUserRsp> Create(CreateUserReq req)
    {
        throw new NotImplementedException();
    }

    public Task<int> Delete(DelReq req)
    {
        throw new NotImplementedException();
    }

    public Task<LoginRsp> Login(LoginReq req)
    {
        throw new NotImplementedException();
    }

    public Task<PagedQueryRsp<QueryUserRsp>> PagedQuery(PagedQueryReq<QueryUserReq> req)
    {
        throw new NotImplementedException();
    }

    public Task<List<QueryUserRsp>> Query(QueryReq<QueryUserReq> req)
    {
        throw new NotImplementedException();
    }

    public Task<QueryUserRsp> Update(UpdateUserReq req)
    {
        throw new NotImplementedException();
    }

    public async Task<QueryUserRsp> UserInfo()
    {
        return await Cache.GetOrCreateAsync( //
            $"{nameof(UserCache)}.{nameof(UserInfo)}.{Service.User.Id}", async entry => {
                SetExpires(entry);
                return await Service.UserInfo();
            });
    }

    private void SetExpires(ICacheEntry entry)
    {
        entry.SetSlidingExpiration(_slidingExpiration);
        entry.AbsoluteExpirationRelativeToNow = _absoluteExpirationRelativeToNow;
    }
}