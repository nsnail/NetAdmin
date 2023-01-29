using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserProfile;

namespace NetAdmin.Application.Modules.Sys;

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
    ///     用户登录
    /// </summary>
    Task<LoginRsp> Login(LoginReq req);

    /// <summary>
    ///     查询用户档案
    /// </summary>
    Task<IEnumerable<QueryUserProfileRsp>> QueryProfile(QueryReq<QueryUserProfileReq> req);

    /// <summary>
    ///     注册用户
    /// </summary>
    Task<RegisterRsp> Register(RegisterReq req);

    /// <summary>
    ///     当前用户信息
    /// </summary>
    Task<QueryUserRsp> UserInfo();
}