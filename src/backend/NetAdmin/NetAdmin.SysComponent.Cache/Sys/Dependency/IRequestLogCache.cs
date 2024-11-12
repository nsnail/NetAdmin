namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     请求日志缓存
/// </summary>
public interface IRequestLogCache : ICache<IDistributedCache, IRequestLogService>, IRequestLogModule;