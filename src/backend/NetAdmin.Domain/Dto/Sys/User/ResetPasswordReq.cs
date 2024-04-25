using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.Dto.Sys.VerifyCode;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：重置密码
/// </summary>
public sealed record ResetPasswordReq : DataAbstraction
{
    /// <summary>
    ///     密码
    /// </summary>
    [Password]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.密码不能为空))]
    public string PasswordText { get; init; }

    /// <summary>
    ///     短信验证请求
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.短信验证请求不能为空))]
    public VerifySmsCodeReq VerifySmsCodeReq { get; init; }
}