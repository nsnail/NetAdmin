using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;

namespace NetAdmin.Host.Caches.Sys;

/// <summary>
///     短信缓存接口
/// </summary>
public interface ISmsCache : ICache<ISmsService>, ISmsModule { }