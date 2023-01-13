using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.User;

namespace NetAdmin.Application.Service.Sys;

/// <summary>
///     用户服务
/// </summary>
public interface IUserService : ICrudService<CreateUserReq, QueryUserRsp // 创建类型
  , QueryUserReq, QueryUserRsp                                           // 查询类型
  , UpdateUserReq, QueryUserRsp                                          // 修改类型
  , NopReq                                                               // 删除类型
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