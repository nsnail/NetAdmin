namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     接口缓存
/// </summary>
public interface IApiCache : ICache<IDistributedCache, IApiService>, IApiModule;