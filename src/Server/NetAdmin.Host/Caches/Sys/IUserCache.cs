using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;

namespace NetAdmin.Host.Caches.Sys;

/// <summary>
///     用户缓存接口
/// </summary>
public interface IUserCache : ICache<IUserService>, IUserModule { }