namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     角色缓存
/// </summary>
public interface IRoleCache : ICache<IDistributedCache, IRoleService>, IRoleModule;