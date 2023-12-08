using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     部门缓存
/// </summary>
public interface IDeptCache : ICache<IDistributedCache, IDeptService>, IDeptModule;