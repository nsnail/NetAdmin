namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     计划作业缓存
/// </summary>
public interface IJobCache : ICache<IDistributedCache, IJobService>, IJobModule;