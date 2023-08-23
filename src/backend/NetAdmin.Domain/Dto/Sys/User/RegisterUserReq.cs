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
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.密码))]
    [Password]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string PasswordText { get; init; }

    /// <summary>
    ///     角色编号列表
    /// </summary>
    [JsonIgnore]
    public IReadOnlyCollection<long> RoleIds { get; init; }

    /// <summary>
    ///     用户名
    /// </summary>
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.用户名))]
    [UserName]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string UserName { get; init; }

    /// <summary>
    ///     短信验证请求
    /// </summary>
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.短信验证请求))]
    public VerifySmsCodeReq VerifySmsCodeReq { get; set; }
}