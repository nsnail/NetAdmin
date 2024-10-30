namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     登录日志缓存
/// </summary>
public interface ILoginLogCache : ICache<IDistributedCache, ILoginLogService>, ILoginLogModule;