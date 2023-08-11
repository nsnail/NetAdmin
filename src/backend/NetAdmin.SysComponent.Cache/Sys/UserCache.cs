using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserProfile;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

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
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> CheckMobileAvailableAsync(CheckMobileAvailableReq req)
    {
        return Service.CheckMobileAvailableAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> CheckUserNameAvailableAsync(CheckUserNameAvailableReq req)
    {
        return Service.CheckUserNameAvailableAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserRsp> CreateAsync(CreateUserReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryUserReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserRsp> GetAsync(QueryUserReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<LoginRsp> LoginByPwdAsync(LoginByPwdReq req)
    {
        return Service.LoginByPwdAsync(req);
    }

    /// <inheritdoc />
    public Task<LoginRsp> LoginBySmsAsync(LoginBySmsReq req)
    {
        return Service.LoginBySmsAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryUserRsp>> PagedQueryAsync(PagedQueryReq<QueryUserReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryUserRsp>> QueryAsync(QueryReq<QueryUserReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryUserProfileRsp>> QueryProfileAsync(QueryReq<QueryUserProfileReq> req)
    {
        return Service.QueryProfileAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserRsp> RegisterAsync(RegisterUserReq req)
    {
        return Service.RegisterAsync(req);
    }

    /// <inheritdoc />
    public Task RemoveCheckMobileAvailableAsync(CheckMobileAvailableReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task RemoveCheckUserNameAvailableAsync(CheckUserNameAvailableReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task RemoveLoginByPwdAsync(LoginByPwdReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task RemoveLoginBySmsAsync(LoginBySmsReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task RemoveQueryProfileAsync(QueryReq<QueryUserProfileReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task RemoveRegisterAsync(RegisterUserReq userReq)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task RemoveResetPasswordAsync(ResetPasswordReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task RemoveUserInfoAsync()
    {
        return RemoveAsync(GetCacheKey( //
                               Service.UserToken.Id.ToString(CultureInfo.InvariantCulture), nameof(UserInfoAsync)));
    }

    /// <inheritdoc />
    public Task ResetPasswordAsync(ResetPasswordReq req)
    {
        return Service.ResetPasswordAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserRsp> SetAvatarAsync(SetAvatarReq req)
    {
        return Service.SetAvatarAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserRsp> SetMobileAsync(SetMobileReq req)
    {
        return Service.SetMobileAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserRsp> UpdateAsync(UpdateUserReq req)
    {
        return Service.UpdateAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserRsp> UserInfoAsync()
    {
        return GetOrCreateAsync( //
            GetCacheKey(Service.UserToken.Id.ToString(CultureInfo.InvariantCulture)), Service.UserInfoAsync
,                                                                                     TimeSpan.FromMinutes(1));
    }
}