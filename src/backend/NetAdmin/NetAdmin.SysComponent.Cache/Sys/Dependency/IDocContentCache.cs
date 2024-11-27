namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     文档内容缓存
/// </summary>
public interface IDocContentCache : ICache<IDistributedCache, IDocContentService>, IDocContentModule;