using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     字典缓存
/// </summary>
public interface IDicCache : ICache<IDistributedCache, IDicService>, IDicModule;