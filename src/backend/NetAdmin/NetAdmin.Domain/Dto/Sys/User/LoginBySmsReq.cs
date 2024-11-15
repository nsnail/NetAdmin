using NetAdmin.Domain.Dto.Sys.VerifyCode;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：短信登录
/// </summary>
public sealed record LoginBySmsReq : VerifySmsCodeReq;