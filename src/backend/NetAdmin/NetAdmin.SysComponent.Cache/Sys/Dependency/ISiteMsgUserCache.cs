namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     站内信-用户映射缓存
/// </summary>
public interface ISiteMsgUserCache : ICache<IDistributedCache, ISiteMsgUserService>, ISiteMsgUserModule;