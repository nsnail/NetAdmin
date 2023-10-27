using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Tpl;
using NetAdmin.SysComponent.Application.Services.Tpl.Dependency;

namespace NetAdmin.SysComponent.Cache.Tpl.Dependency;

/// <summary>
///     示例缓存
/// </summary>
public interface IExampleCache : ICache<IDistributedCache, IExampleService>, IExampleModule { }