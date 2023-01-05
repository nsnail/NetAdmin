using NetAdmin.DataContract.Dto.Sys.User;

namespace NetAdmin.Api.Sys;

/// <summary>
///     用户接口
/// </summary>
public interface IUserApi : IRestfulApi
{
    /// <summary>
    ///     创建用户
    /// </summary>
    Task CreateUser(CreateUserReq req);

    /// <summary>
    ///     用户登录
    /// </summary>
    Task<LoginRsp> Login(LoginReq req);
}