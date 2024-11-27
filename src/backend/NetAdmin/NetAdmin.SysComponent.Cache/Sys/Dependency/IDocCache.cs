namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     文档缓存
/// </summary>
public interface IDocCache : ICache<IDistributedCache, IDocService>, IDocModule;