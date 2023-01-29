using NetAdmin.Domain.Dto.Sys.Sms;

namespace NetAdmin.Application.Modules.Sys;

/// <summary>
///     短信模块
/// </summary>
public interface ISmsModule
{
    /// <summary>
    ///     发送短信验证码
    /// </summary>
    Task<SendSmsCodeRsp> SendSmsCode(SendSmsCodeReq req);

    /// <summary>
    ///     完成短信验证
    /// </summary>
    Task<bool> VerifySmsCode(VerifySmsCodeReq req);
}