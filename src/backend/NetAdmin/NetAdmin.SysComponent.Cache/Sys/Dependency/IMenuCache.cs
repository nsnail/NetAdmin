namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     菜单缓存
/// </summary>
public interface IMenuCache : ICache<IDistributedCache, IMenuService>, IMenuModule;