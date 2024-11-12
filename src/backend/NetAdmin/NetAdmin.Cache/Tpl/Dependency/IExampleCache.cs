using NetAdmin.Application.Modules.Tpl;
using NetAdmin.Application.Services.Tpl.Dependency;

namespace NetAdmin.Cache.Tpl.Dependency;

/// <summary>
///     示例缓存
/// </summary>
public interface IExampleCache : ICache<IDistributedCache, IExampleService>, IExampleModule;