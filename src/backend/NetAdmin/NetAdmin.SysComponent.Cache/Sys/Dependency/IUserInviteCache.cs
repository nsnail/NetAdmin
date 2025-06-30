namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     用户邀请缓存
/// </summary>
public interface IUserInviteCache : ICache<IDistributedCache, IUserInviteService>, IUserInviteModule;