namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     文件缓存
/// </summary>
public interface IFileCache : ICache<IDistributedCache, IFileService>, IFileModule;