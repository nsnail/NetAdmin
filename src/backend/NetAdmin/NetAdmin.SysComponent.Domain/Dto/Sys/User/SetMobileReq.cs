using NetAdmin.SysComponent.Domain.Dto.Sys.VerifyCode;

namespace NetAdmin.SysComponent.Domain.Dto.Sys.User;

/// <summary>
///     请求：设置手机号
/// </summary>
public sealed record SetMobileReq : DataAbstraction
{
    /// <summary>
    ///     新手机短信验证请求
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.短信验证请求不能为空))]
    public VerifySmsCodeReq NewVerifySmsCodeReq { get; init; }

    /// <summary>
    ///     原手机短信验证请求
    /// </summary>
    public VerifySmsCodeReq OriginVerifySmsCodeReq { get; init; }
}