using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     用户服务
/// </summary>
public interface IUserService : IService, IUserModule
{
    /// <summary>
    ///     用户是否存在
    /// </summary>
    public Task<bool> ExistAsync(QueryReq<QueryUserReq> req);

    /// <summary>
    ///     用户编号登录
    /// </summary>
    Task<LoginRsp> LoginByUserIdAsync(long userId);
}