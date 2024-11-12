namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     部门缓存
/// </summary>
public interface IDeptCache : ICache<IDistributedCache, IDeptService>, IDeptModule;