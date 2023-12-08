using NetAdmin.Application.Services;
using NetAdmin.SysComponent.Application.Modules.Sys;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     缓存服务
/// </summary>
public interface ICacheService : IService, ICacheModule;