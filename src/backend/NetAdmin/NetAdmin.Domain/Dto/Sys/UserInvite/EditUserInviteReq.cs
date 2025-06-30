namespace NetAdmin.Domain.Dto.Sys.UserInvite;

/// <summary>
///     请求：编辑用户邀请
/// </summary>
public record EditUserInviteReq : CreateUserInviteReq
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}