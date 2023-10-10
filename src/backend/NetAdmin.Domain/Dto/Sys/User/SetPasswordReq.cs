using NetAdmin.Domain.Attributes.DataValidation;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：设置密码
/// </summary>
public sealed record SetPasswordReq : DataAbstraction
{
    /// <summary>
    ///     新密码
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.新密码不能为空))]
    [Password]
    public string NewPassword { get; init; }

    /// <summary>
    ///     旧密码
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.旧密码不能为空))]
    [Password]
    public string OldPassword { get; init; }
}