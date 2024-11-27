namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     文档分类缓存
/// </summary>
public interface IDocCatalogCache : ICache<IDistributedCache, IDocCatalogService>, IDocCatalogModule;