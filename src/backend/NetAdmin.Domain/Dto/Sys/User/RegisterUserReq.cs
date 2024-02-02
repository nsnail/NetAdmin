using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.VerifyCode;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：注册用户
/// </summary>
public record RegisterUserReq : Sys_User
{
    /// <summary>
    ///     密码
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.密码不能为空))]
    [Password]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string PasswordText { get; init; }

    /// <summary>
    ///     角色编号列表
    /// </summary>
    [JsonIgnore]
    public IReadOnlyCollection<long> RoleIds { get; init; }

    /// <inheritdoc cref="Sys_User.UserName" />
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.用户名不能为空))]
    [UserName]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string UserName { get; init; }

    /// <summary>
    ///     短信验证请求
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.短信验证请求不能为空))]
    public VerifySmsCodeReq VerifySmsCodeReq { get; init; }
}