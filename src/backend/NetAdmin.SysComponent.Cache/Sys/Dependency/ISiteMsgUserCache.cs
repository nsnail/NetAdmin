using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     站内信-用户映射缓存
/// </summary>
public interface ISiteMsgUserCache : ICache<IDistributedCache, ISiteMsgUserService>, ISiteMsgUserModule { }