using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     缓存缓存
/// </summary>
public interface ICacheCache : ICache<IDistributedCache, ICacheService>, ICacheModule;