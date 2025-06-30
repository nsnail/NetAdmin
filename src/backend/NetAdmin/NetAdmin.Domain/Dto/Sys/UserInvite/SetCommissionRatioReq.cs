namespace NetAdmin.Domain.Dto.Sys.UserInvite;

/// <summary>
///     请求：设置返佣比率
/// </summary>
public record SetCommissionRatioReq : CreateUserInviteReq
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}