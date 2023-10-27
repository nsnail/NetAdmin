using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     菜单缓存
/// </summary>
public interface IMenuCache : ICache<IDistributedCache, IMenuService>, IMenuModule { }