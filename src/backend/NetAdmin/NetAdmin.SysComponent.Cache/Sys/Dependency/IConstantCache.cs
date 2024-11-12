namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     常量缓存
/// </summary>
public interface IConstantCache : ICache<IDistributedCache, IConstantService>, IConstantModule;