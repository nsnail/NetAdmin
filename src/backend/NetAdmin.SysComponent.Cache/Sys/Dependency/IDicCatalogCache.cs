using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     字典目录缓存
/// </summary>
public interface IDicCatalogCache : ICache<IDistributedCache, IDicCatalogService>, IDicCatalogModule { }