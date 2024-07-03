using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserProfile;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IUserCache" />
public sealed class UserCache(IDistributedCache cache, IUserService service, IVerifyCodeCache verifyCodeCache) //
    : DistributedCache<IUserService>(cache, service), IScoped, IUserCache
{
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
    public Task<long> CountAsync(QueryReq<QueryUserReq> req)
    {
        return Service.CountAsync(req);
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
    public Task<QueryUserRsp> EditAsync(EditUserReq req)
    {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryUserReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryUserReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryUserRsp> GetAsync(QueryUserReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<GetSessionUserAppConfigRsp> GetSessionUserAppConfigAsync()
    {
        return Service.GetSessionUserAppConfigAsync();
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
        #if !DEBUG
        return GetOrCreateAsync(                                                  //
            GetCacheKey(req.GetHashCode().ToString(CultureInfo.InvariantCulture)) //
          , () => Service.QueryAsync(req), TimeSpan.FromSeconds(Numbers.SECS_CACHE_DEFAULT));
        #else
        return Service.QueryAsync(req);
        #endif
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryUserProfileRsp>> QueryProfileAsync(QueryReq<QueryUserProfileReq> req)
    {
        return Service.QueryProfileAsync(req);
    }

    /// <inheritdoc />
    public Task<UserInfoRsp> RegisterAsync(RegisterUserReq req)
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
    public Task RemoveRegisterAsync(RegisterUserReq req)
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
    public Task<int> ResetPasswordAsync(ResetPasswordReq req)
    {
        return Service.ResetPasswordAsync(req);
    }

    /// <inheritdoc />
    public Task<UserInfoRsp> SetAvatarAsync(SetAvatarReq req)
    {
        return Service.SetAvatarAsync(req);
    }

    /// <inheritdoc />
    public async Task<UserInfoRsp> SetEmailAsync(SetEmailReq req)
    {
        return !await verifyCodeCache.VerifyAsync(req).ConfigureAwait(false)
            ? throw new NetAdminInvalidOperationException(Ln.邮箱验证码不正确)
            : await Service.SetEmailAsync(req).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public Task<int> SetEnabledAsync(SetUserEnabledReq req)
    {
        return Service.SetEnabledAsync(req);
    }

    /// <inheritdoc />
    public Task<UserInfoRsp> SetMobileAsync(SetMobileReq req)
    {
        return Service.SetMobileAsync(req);
    }

    /// <inheritdoc />
    public Task<int> SetPasswordAsync(SetPasswordReq req)
    {
        return Service.SetPasswordAsync(req);
    }

    /// <inheritdoc />
    public Task<int> SetSessionUserAppConfigAsync(SetSessionUserAppConfigReq req)
    {
        return Service.SetSessionUserAppConfigAsync(req);
    }

    /// <inheritdoc />
    public Task<UserInfoRsp> UserInfoAsync()
    {
        #if !DEBUG
        return GetOrCreateAsync( //
            GetCacheKey(Service.UserToken.Id.ToString(CultureInfo.InvariantCulture)), Service.UserInfoAsync
          , TimeSpan.FromSeconds(Numbers.SECS_CACHE_DEFAULT));
        #else
        return Service.UserInfoAsync();
        #endif
    }
}