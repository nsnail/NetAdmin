namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     字典目录缓存
/// </summary>
public interface IDicCatalogCache : ICache<IDistributedCache, IDicCatalogService>, IDicCatalogModule;