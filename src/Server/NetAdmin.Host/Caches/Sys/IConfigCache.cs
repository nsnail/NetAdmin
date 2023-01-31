using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;

namespace NetAdmin.Host.Caches.Sys;

/// <summary>
///     配置缓存接口
/// </summary>
public interface IConfigCache : ICache<IConfigService>, IConfigModule { }