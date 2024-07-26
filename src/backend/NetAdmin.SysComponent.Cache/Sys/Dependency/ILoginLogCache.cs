using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     登录日志缓存
/// </summary>
public interface ILoginLogCache : ICache<IDistributedCache, ILoginLogService>, ILoginLogModule;