using NetAdmin.Application.Services;
using NetAdmin.SysComponent.Application.Modules.Sys;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     验证码服务
/// </summary>
public interface IVerifyCodeService : IService, IVerifyCodeModule;