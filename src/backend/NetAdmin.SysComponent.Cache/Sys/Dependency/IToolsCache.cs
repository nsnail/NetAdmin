namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     工具缓存
/// </summary>
public interface IToolsCache : ICache<IDistributedCache, IToolsService>, IToolsModule;