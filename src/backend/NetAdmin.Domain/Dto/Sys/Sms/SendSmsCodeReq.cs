using System.ComponentModel.DataAnnotations;
using NetAdmin.Domain.Dto.Sys.Captcha;

namespace NetAdmin.Domain.Dto.Sys.Sms;

/// <summary>
///     请求：发送短信验证码
/// </summary>
public record SendSmsCodeReq : SmsCodeInfo
{
    /// <summary>
    ///     类型
    /// </summary>
    [Required]
    public Types Type { get; init; }

    /// <summary>
    ///     人机校验请求
    /// </summary>
    [Required]
    public VerifyCaptchaReq VerifyCaptchaReq { get; init; }
}