using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     常量缓存
/// </summary>
public interface IConstantCache : ICache<IDistributedCache, IConstantService>, IConstantModule { }