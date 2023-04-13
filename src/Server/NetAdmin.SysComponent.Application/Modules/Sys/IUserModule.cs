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
    Task<LoginRsp> PwdLoginAsync(PwdLoginReq req);

    /// <summary>
    ///     查询用户档案
    /// </summary>
    Task<IEnumerable<QueryUserProfileRsp>> QueryProfileAsync(QueryReq<QueryUserProfileReq> req);

    /// <summary>
    ///     注册用户
    /// </summary>
    Task RegisterAsync(RegisterReq req);

    /// <summary>
    ///     重设密码
    /// </summary>
    Task ResetPasswordAsync(ResetPasswordReq req);

    /// <summary>
    ///     短信登录
    /// </summary>
    Task<LoginRsp> SmsLoginAsync(SmsLoginReq req);

    /// <summary>
    ///     当前用户信息
    /// </summary>
    Task<QueryUserRsp> UserInfoAsync();
}