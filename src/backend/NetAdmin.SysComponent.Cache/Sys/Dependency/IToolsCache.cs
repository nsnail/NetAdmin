using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     工具缓存
/// </summary>
public interface IToolsCache : ICache<IDistributedCache, IToolsService>, IToolsModule;