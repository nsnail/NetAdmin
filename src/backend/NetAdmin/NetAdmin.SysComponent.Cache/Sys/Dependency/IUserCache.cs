namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     用户缓存
/// </summary>
public interface IUserCache : ICache<IDistributedCache, IUserService>, IUserModule
{
    /// <summary>
    ///     删除缓存 UserInfoAsync
    /// </summary>
    Task RemoveUserInfoAsync();
}