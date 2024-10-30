namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     配置缓存
/// </summary>
public interface IConfigCache : ICache<IDistributedCache, IConfigService>, IConfigModule;