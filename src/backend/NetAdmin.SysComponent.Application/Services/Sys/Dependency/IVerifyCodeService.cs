using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Sys.VerifyCode;
using NetAdmin.SysComponent.Application.Modules.Sys;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     验证码服务
/// </summary>
public interface IVerifyCodeService : IService, IVerifyCodeModule
{
    /// <summary>
    ///     设置验证码状态
    /// </summary>
    Task<int> SetVerifyCodeStatusAsync(SetVerifyCodeStatusReq req);
}