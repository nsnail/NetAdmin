using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.User;

namespace NetAdmin.Api.Sys;

/// <summary>
///     用户接口
/// </summary>
public interface IUserApi : ICrudApi<CreateUserReq         // 创建类型
                              , QueryUserReq, QueryUserRsp // 查询类型
                              , NopReq                     // 修改类型
                              , NopReq                     // 删除类型
                            >, IRestfulApi
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