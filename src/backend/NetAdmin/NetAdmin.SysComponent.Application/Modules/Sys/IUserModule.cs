using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserProfile;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     用户模块
/// </summary>
public partial interface IUserModule : ICrudModule<CreateUserReq, QueryUserRsp // 创建类型
  , EditUserReq                                                                // 编辑类型
  , QueryUserReq, QueryUserRsp                                                 // 查询类型
  , DelReq                                                                     // 删除类型
>
{
    /// <summary>
    ///     检查手机号码是否可用
    /// </summary>
    Task<bool> CheckMobileAvailableAsync(CheckMobileAvailableReq req);

    /// <summary>
    ///     检查用户名是否可用
    /// </summary>
    Task<bool> CheckUserNameAvailableAsync(CheckUserNameAvailableReq req);

    /// <summary>
    ///     密码登录
    /// </summary>
    Task<LoginRsp> LoginByPwdAsync(LoginByPwdReq req);

    /// <summary>
    ///     短信登录
    /// </summary>
    Task<LoginRsp> LoginBySmsAsync(LoginBySmsReq req);

    /// <summary>
    ///     查询用户档案
    /// </summary>
    Task<IEnumerable<QueryUserProfileRsp>> QueryProfileAsync(QueryReq<QueryUserProfileReq> req);

    /// <summary>
    ///     注册用户
    /// </summary>
    Task<UserInfoRsp> RegisterAsync(RegisterUserReq req);

    /// <summary>
    ///     重设密码
    /// </summary>
    Task<int> ResetPasswordAsync(ResetPasswordReq req);

    /// <summary>
    ///     设置用户头像
    /// </summary>
    Task<UserInfoRsp> SetAvatarAsync(SetAvatarReq req);

    /// <summary>
    ///     设置邮箱
    /// </summary>
    Task<UserInfoRsp> SetEmailAsync(SetEmailReq req);

    /// <summary>
    ///     启用/禁用用户
    /// </summary>
    Task<int> SetEnabledAsync(SetUserEnabledReq req);

    /// <summary>
    ///     设置手机号码
    /// </summary>
    Task<UserInfoRsp> SetMobileAsync(SetMobileReq req);

    /// <summary>
    ///     设置密码
    /// </summary>
    Task<int> SetPasswordAsync(SetPasswordReq req);

    /// <summary>
    ///     当前用户信息
    /// </summary>
    Task<UserInfoRsp> UserInfoAsync();
}