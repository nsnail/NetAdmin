namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     代码模板缓存
/// </summary>
public interface ICodeTemplateCache : ICache<IDistributedCache, ICodeTemplateService>, ICodeTemplateModule;