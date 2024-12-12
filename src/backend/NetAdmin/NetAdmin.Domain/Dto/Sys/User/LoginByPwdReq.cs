namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：密码登录
/// </summary>
public sealed record LoginByPwdReq : DataAbstraction
{
    /// <summary>
    ///     用户名、手机号、邮箱
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.账号不能为空))]
    public string Account { get; init; }

    /// <inheritdoc cref="Sys_User.Password" />
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.密码不能为空))]
    public string Password { get; init; }
}