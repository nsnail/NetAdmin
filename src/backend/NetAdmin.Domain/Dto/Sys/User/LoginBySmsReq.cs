using NetAdmin.Domain.Dto.Sys.Sms;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：短信登录
/// </summary>
public record LoginBySmsReq : VerifySmsCodeReq;