using NetAdmin.SysComponent.Domain.Dto.Sys.User;

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