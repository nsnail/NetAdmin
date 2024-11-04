using NetAdmin.SysComponent.Domain.Dto.Sys.VerifyCode;

namespace NetAdmin.SysComponent.Domain.Dto.Sys.User;

/// <summary>
///     请求：短信登录
/// </summary>
public sealed record LoginBySmsReq : VerifySmsCodeReq;