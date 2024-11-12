using NetAdmin.SysComponent.Domain.Dto.Sys.VerifyCode;

namespace NetAdmin.SysComponent.Domain.Dto.Sys.User;

/// <summary>
///     请求：注册用户
/// </summary>
public sealed record RegisterUserReq : Sys_User
{
    /// <summary>
    ///     密码
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Password]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.密码不能为空))]
    public string PasswordText { get; init; }

    /// <summary>
    ///     角色编号列表
    /// </summary>
    [JsonIgnore]
    public IReadOnlyCollection<long> RoleIds { get; init; }

    /// <inheritdoc cref="Sys_User.UserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.用户名不能为空))]
    [UserName]
    public override string UserName { get; init; }

    /// <summary>
    ///     短信验证请求
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.短信验证请求不能为空))]
    public VerifySmsCodeReq VerifySmsCodeReq { get; init; }
}