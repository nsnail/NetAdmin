namespace NetAdmin.Domain.Dto.Sys.UserInvite;

/// <summary>
///     请求：设置允许自助充值
/// </summary>
public record SetSelfDepositAllowedReq : Sys_UserInvite
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="Sys_UserInvite.SelfDepositAllowed" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool SelfDepositAllowed { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}