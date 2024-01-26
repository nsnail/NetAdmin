using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：更新用户头像
/// </summary>
public sealed record SetAvatarReq : Sys_User
{
    /// <inheritdoc cref="Sys_User.Avatar" />
    [Url(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.网络地址不正确))]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.用户头像不能为空))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Avatar { get; init; }
}