using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Sms;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：注册用户
/// </summary>
public record RegisterReq : TbSysUser
{
    /// <summary>
    ///     已激活
    /// </summary>
    [JsonIgnore]
    public virtual bool Activated { get; init; }

    /// <summary>
    ///     密码
    /// </summary>
    [Required]
    [Password]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string PasswordText { get; init; }

    /// <summary>
    ///     岗位id列表
    /// </summary>
    [JsonIgnore]
    public virtual IReadOnlyCollection<long> PositionIds { get; init; }

    /// <summary>
    ///     角色id列表
    /// </summary>
    [JsonIgnore]
    public virtual IReadOnlyCollection<long> RoleIds { get; init; }

    /// <inheritdoc cref="TbSysUser.UserName" />
    [Required]
    [UserName]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string UserName { get; init; }

    /// <summary>
    ///     短信验证请求
    /// </summary>
    [Required]
    public VerifySmsCodeReq VerifySmsCodeReq { get; init; }
}