namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     站内信缓存
/// </summary>
public interface ISiteMsgCache : ICache<IDistributedCache, ISiteMsgService>, ISiteMsgModule;