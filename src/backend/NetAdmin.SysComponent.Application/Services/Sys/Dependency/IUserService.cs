using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.SysComponent.Application.Modules.Sys;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     用户服务
/// </summary>
public interface IUserService : IService, IUserModule
{
    /// <summary>
    ///     获取单个用户（带更新锁）
    /// </summary>
    Task<QueryUserRsp> GetForUpdateAsync(QueryUserReq req);

    /// <summary>
    ///     用户编号登录
    /// </summary>
    Task<LoginRsp> LoginByUserIdAsync(long userId);

    /// <summary>
    ///     单体更新
    /// </summary>
    Task UpdateSingleAsync(UpdateUserReq req);
}