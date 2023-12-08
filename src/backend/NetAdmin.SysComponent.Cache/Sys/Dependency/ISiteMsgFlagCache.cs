using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     站内信标记缓存
/// </summary>
public interface ISiteMsgFlagCache : ICache<IDistributedCache, ISiteMsgFlagService>, ISiteMsgFlagModule;