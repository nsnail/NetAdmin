using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserProfile;

#pragma warning disable CS1591

namespace NetAdmin.Host.Caches.Sys.Implements;

/// <inheritdoc cref="IUserCache" />
public class UserCache : CacheBase<IUserService>, IScoped, IUserCache
{
    private readonly TimeSpan _absoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
    private readonly TimeSpan _slidingExpiration               = TimeSpan.FromMinutes(5);

    public UserCache(IMemoryCache cache, IUserService service) //
        : base(cache, service) { }

    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckMobileAvaliableAsync(CheckMobileAvaliableReq req)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckUserNameAvaliableAsync(CheckUserNameAvaliableReq req)
    {
        throw new NotImplementedException();
    }

    public Task<QueryUserRsp> CreateAsync(CreateUserReq req)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(DelReq req)
    {
        throw new NotImplementedException();
    }

    public Task<PagedQueryRsp<QueryUserRsp>> PagedQueryAsync(PagedQueryReq<QueryUserReq> req)
    {
        throw new NotImplementedException();
    }

    public Task<LoginRsp> PwdLoginAsync(PwdLoginReq req)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<QueryUserRsp>> QueryAsync(QueryReq<QueryUserReq> req)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<QueryUserProfileRsp>> QueryProfileAsync(QueryReq<QueryUserProfileReq> req)
    {
        throw new NotImplementedException();
    }

    public Task RegisterAsync(RegisterReq req)
    {
        throw new NotImplementedException();
    }

    public Task ResetPasswordAsync(ResetPasswordReq req)
    {
        throw new NotImplementedException();
    }

    public Task<LoginRsp> SmsLoginAsync(SmsLoginReq req)
    {
        throw new NotImplementedException();
    }

    public Task<QueryUserRsp> UpdateAsync(UpdateUserReq req)
    {
        throw new NotImplementedException();
    }

    public Task<QueryUserRsp> UserInfoAsync()
    {
        return Cache.GetOrCreateAsync( //
            $"{nameof(UserCache)}.{nameof(UserInfoAsync)}.{Service.User.Id}", async entry => {
                SetExpires(entry);
                return await Service.UserInfoAsync();
            });
    }

    private void SetExpires(ICacheEntry entry)
    {
        _                                     = entry.SetSlidingExpiration(_slidingExpiration);
        entry.AbsoluteExpirationRelativeToNow = _absoluteExpirationRelativeToNow;
    }
}