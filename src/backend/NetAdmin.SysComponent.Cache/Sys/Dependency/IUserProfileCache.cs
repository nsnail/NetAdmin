using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     用户档案缓存
/// </summary>
public interface IUserProfileCache : ICache<IDistributedCache, IUserProfileService>, IUserProfileModule;