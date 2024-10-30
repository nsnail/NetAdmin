namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     请求日志明细缓存
/// </summary>
public interface IRequestLogDetailCache : ICache<IDistributedCache, IRequestLogDetailService>, IRequestLogDetailModule;