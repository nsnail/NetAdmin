using NetAdmin.Application.Services;
using NetAdmin.SysComponent.Application.Modules.Sys;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     人机验证服务
/// </summary>
public interface ICaptchaService : IService, ICaptchaModule { }