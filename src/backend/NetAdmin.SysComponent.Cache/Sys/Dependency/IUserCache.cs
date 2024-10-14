using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     用户缓存
/// </summary>
public interface IUserCache : ICache<IDistributedCache, IUserService>, IUserModule
{
    /// <summary>
    ///     用户编号登录
    /// </summary>
    Task<LoginRsp> LoginByUserIdAsync(long userId);

    /// <summary>
    ///     删除缓存 UserInfoAsync
    /// </summary>
    Task RemoveUserInfoAsync();
}