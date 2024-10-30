namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     站内信标记缓存
/// </summary>
public interface ISiteMsgFlagCache : ICache<IDistributedCache, ISiteMsgFlagService>, ISiteMsgFlagModule;