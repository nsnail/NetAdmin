namespace NetAdmin.SysComponent.Domain.Dto.Sys.User;

/// <summary>
///     请求：设置用户头像
/// </summary>
public sealed record SetAvatarReq : Sys_User
{
    /// <inheritdoc cref="Sys_User.Avatar" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.用户头像不能为空))]
    [Url(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.网络地址不正确))]
    public override string Avatar { get; init; }

    /// <inheritdoc cref="System.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}