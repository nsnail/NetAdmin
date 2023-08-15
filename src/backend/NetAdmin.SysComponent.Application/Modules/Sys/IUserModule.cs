using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserProfile;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     用户模块
/// </summary>
public interface IUserModule : ICrudModule<CreateUserReq, QueryUserRsp // 创建类型
  , QueryUserReq, QueryUserRsp                                         // 查询类型
  , UpdateUserReq, QueryUserRsp                                        // 修改类型
  , DelReq                                                             // 删除类型
>
{
    /// <summary>
    ///     检查手机号是否可用
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
    Task<uint> ResetPasswordAsync(ResetPasswordReq req);

    /// <summary>
    ///     设置用户头像
    /// </summary>
    Task<UserInfoRsp> SetAvatarAsync(SetAvatarReq req);

    /// <summary>
    ///     设置手机号
    /// </summary>
    Task<UserInfoRsp> SetMobileAsync(SetMobileReq req);

    /// <summary>
    ///     当前用户信息
    /// </summary>
    Task<UserInfoRsp> UserInfoAsync();
}