using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     请求日志缓存
/// </summary>
public interface IRequestLogCache : ICache<IDistributedCache, IRequestLogService>, IRequestLogModule { }