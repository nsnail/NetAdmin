namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     用户档案缓存
/// </summary>
public interface IUserProfileCache : ICache<IDistributedCache, IUserProfileService>, IUserProfileModule;