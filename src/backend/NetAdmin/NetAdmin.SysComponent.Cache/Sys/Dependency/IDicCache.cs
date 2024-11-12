namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     字典缓存
/// </summary>
public interface IDicCache : ICache<IDistributedCache, IDicService>, IDicModule;