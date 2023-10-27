using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     角色缓存
/// </summary>
public interface IRoleCache : ICache<IDistributedCache, IRoleService>, IRoleModule { }