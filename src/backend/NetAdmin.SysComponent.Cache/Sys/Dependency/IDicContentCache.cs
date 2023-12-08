using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     字典内容缓存
/// </summary>
public interface IDicContentCache : ICache<IDistributedCache, IDicContentService>, IDicContentModule;