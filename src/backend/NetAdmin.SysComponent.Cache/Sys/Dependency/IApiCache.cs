using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     接口缓存
/// </summary>
public interface IApiCache : ICache<IDistributedCache, IApiService>, IApiModule { }