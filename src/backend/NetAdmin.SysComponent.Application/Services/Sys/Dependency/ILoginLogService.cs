using NetAdmin.Application.Services;
using NetAdmin.SysComponent.Application.Modules.Sys;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     登录日志服务
/// </summary>
public interface ILoginLogService : IService, ILoginLogModule;