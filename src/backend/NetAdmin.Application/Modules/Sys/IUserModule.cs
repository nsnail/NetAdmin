using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.User;

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
    ///     当前用户信息
    /// </summary>
    Task<QueryUserRsp> UserInfo();
}