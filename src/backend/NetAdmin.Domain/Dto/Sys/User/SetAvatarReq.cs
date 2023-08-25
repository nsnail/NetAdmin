using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：更新用户头像
/// </summary>
public sealed record SetAvatarReq : Sys_User
{
    /// <inheritdoc cref="Sys_User.Avatar" />
    [CultureUrl]
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.用户头像))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Avatar { get; init; }
}