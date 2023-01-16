using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NetAdmin.DataContract.Attributes.DataValidation;
using NetAdmin.DataContract.DbMaps.Sys;

namespace NetAdmin.DataContract.Dto.Sys.User;

/// <summary>
///     请求：用户登录
/// </summary>
public record LoginReq : TbSysUser
{
    /// <inheritdoc cref="TbSysUser.Password" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    [Password]
    public new string Password { get; init; }

    /// <inheritdoc cref="TbSysUser.UserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    [UserName]
    public override string UserName { get; init; }
}