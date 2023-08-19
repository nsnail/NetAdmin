using NetAdmin.Domain.Dto.Sys.Email;
using NetAdmin.Domain.Dto.Sys.Sms;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：设置邮箱
/// </summary>
public sealed record SetEmailReq : VerifyEmailCodeReq
{
    /// <summary>
    ///     短信验证请求
    /// </summary>
    public VerifySmsCodeReq VerifySmsCodeReq { get; init; }
}