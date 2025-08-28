namespace NetAdmin.Domain.Dto.Sys.UserInvite;

/// <summary>
///     请求：创建用户邀请
/// </summary>
public record CreateUserInviteReq : Sys_UserInvite
{
    /// <inheritdoc cref="Sys_UserInvite.CommissionRatio" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int CommissionRatio { get; init; }

    /// <inheritdoc />
    public override bool SelfDepositAllowed { get; init; } = true;
}