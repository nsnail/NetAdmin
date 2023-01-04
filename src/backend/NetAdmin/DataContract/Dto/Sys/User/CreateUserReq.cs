using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NetAdmin.Aop.Attributes;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.Dto.Sys.User;

/// <summary>
///     请求：创建用户
/// </summary>
public record CreateUserReq : TbSysUser
{
    /// <inheritdoc cref="TbSysUser.Password" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [RequiredField]
    [RegularExpression(Strings.REGEX_PASSWORD, ErrorMessage = Strings.MSG_PASSWORD_STRONG)]
    public new string Password { get; set; }

    /// <summary>
    ///     角色id列表
    /// </summary>
    [RequiredField]
    public List<long> RoleIds { get; set; }

    /// <inheritdoc cref="TbSysUser.UserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [RequiredField]
    [RegularExpression(Strings.REGEX_USERNAME, ErrorMessage = Strings.MSG_USERNAME_STRONG)]
    public override string UserName { get; set; }
}