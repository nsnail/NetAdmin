namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     开发缓存
/// </summary>
public interface IDevCache : ICache<IDistributedCache, IDevService>, IDevModule;