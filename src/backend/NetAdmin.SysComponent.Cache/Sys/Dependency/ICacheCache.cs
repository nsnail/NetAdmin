namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     缓存缓存
/// </summary>
public interface ICacheCache : ICache<IDistributedCache, ICacheService>, ICacheModule;