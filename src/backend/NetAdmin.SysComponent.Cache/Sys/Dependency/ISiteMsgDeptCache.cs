using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     站内信-部门映射缓存
/// </summary>
public interface ISiteMsgDeptCache : ICache<IDistributedCache, ISiteMsgDeptService>, ISiteMsgDeptModule;