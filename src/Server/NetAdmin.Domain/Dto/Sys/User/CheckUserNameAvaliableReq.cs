using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：检查用户名是否可用
/// </summary>
public record CheckUserNameAvaliableReq : TbSysUser
{
    /// <inheritdoc cref="TbSysUser.UserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    [UserName]
    public override string UserName { get; init; }
}