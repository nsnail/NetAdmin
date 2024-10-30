namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     站内信-角色映射缓存
/// </summary>
public interface ISiteMsgRoleCache : ICache<IDistributedCache, ISiteMsgRoleService>, ISiteMsgRoleModule;