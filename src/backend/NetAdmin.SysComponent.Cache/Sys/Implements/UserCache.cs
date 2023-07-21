using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserProfile;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Implements;

/// <inheritdoc cref="IUserCache" />
public sealed class UserCache : DistributedCache<IUserService>, IScoped, IUserCache
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserCache" /> class.
    /// </summary>
    public UserCache(IDistributedCache cache, IUserService service) //
        : base(cache, service) { }

    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<bool> CheckMobileAvailableAsync(CheckMobileAvailableReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<bool> CheckUserNameAvailableAsync(CheckUserNameAvailableReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryUserRsp> CreateAsync(CreateUserReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryUserReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryUserRsp> GetAsync(QueryUserReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<LoginRsp> LoginByPwdAsync(LoginByPwdReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<LoginRsp> LoginBySmsAsync(LoginBySmsReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryUserRsp>> PagedQueryAsync(PagedQueryReq<QueryUserReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryUserRsp>> QueryAsync(QueryReq<QueryUserReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryUserProfileRsp>> QueryProfileAsync(QueryReq<QueryUserProfileReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryUserRsp> RegisterAsync(RegisterUserReq userReq)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task ResetPasswordAsync(ResetPasswordReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryUserRsp> UpdateAsync(UpdateUserReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryUserRsp> UserInfoAsync()
    {
        return GetOrCreateAsync( //
            GetCacheKey(Service.UserToken.Id.ToString(CultureInfo.InvariantCulture)), Service.UserInfoAsync
,                                                                                     TimeSpan.FromMinutes(1));
    }
}