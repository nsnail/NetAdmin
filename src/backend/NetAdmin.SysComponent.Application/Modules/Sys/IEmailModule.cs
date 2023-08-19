using NetAdmin.Domain.Dto.Sys.Email;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     邮件模块
/// </summary>
public interface IEmailModule
{
    /// <summary>
    ///     发送邮箱验证码
    /// </summary>
    Task<SendEmailCodeRsp> SendEmailCodeAsync(SendEmailCodeReq req);

    /// <summary>
    ///     完成邮箱验证
    /// </summary>
    /// <remarks>
    ///     对于验证失败的，不主动删除缓存，通过防火墙来应对暴力破解
    /// </remarks>
    Task<bool> VerifyEmailCodeAsync(VerifyEmailCodeReq req);
}