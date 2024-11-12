namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     站内信-部门映射缓存
/// </summary>
public interface ISiteMsgDeptCache : ICache<IDistributedCache, ISiteMsgDeptService>, ISiteMsgDeptModule;