using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <summary>
///     用户缓存
/// </summary>
public interface IUserCache : ICache<IDistributedCache, IUserService>, IUserModule { }