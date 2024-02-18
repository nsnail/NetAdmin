using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserProfile;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     用户缓存
/// </summary>
public interface IUserCache : ICache<IDistributedCache, IUserService>, IUserModule
{
    /// <summary>
    ///     删除缓存 CheckMobileAvailableAsync
    /// </summary>
    Task RemoveCheckMobileAvailableAsync(CheckMobileAvailableReq req);

    /// <summary>
    ///     删除缓存 CheckUserNameAvailableAsync
    /// </summary>
    Task RemoveCheckUserNameAvailableAsync(CheckUserNameAvailableReq req);

    /// <summary>
    ///     删除缓存 LoginByPwdAsync
    /// </summary>
    Task RemoveLoginByPwdAsync(LoginByPwdReq req);

    /// <summary>
    ///     删除缓存 LoginBySmsAsync
    /// </summary>
    Task RemoveLoginBySmsAsync(LoginBySmsReq req);

    /// <summary>
    ///     删除缓存 QueryProfileAsync
    /// </summary>
    Task RemoveQueryProfileAsync(QueryReq<QueryUserProfileReq> req);

    /// <summary>
    ///     删除缓存 RegisterAsync
    /// </summary>
    Task RemoveRegisterAsync(RegisterUserReq req);

    /// <summary>
    ///     删除缓存 ResetPasswordAsync
    /// </summary>
    Task RemoveResetPasswordAsync(ResetPasswordReq req);

    /// <summary>
    ///     删除缓存 UserInfoAsync
    /// </summary>
    Task RemoveUserInfoAsync();
}