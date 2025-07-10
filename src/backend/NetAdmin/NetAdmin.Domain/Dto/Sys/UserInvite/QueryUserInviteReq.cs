namespace NetAdmin.Domain.Dto.Sys.UserInvite;

/// <summary>
///     请求：查询用户邀请
/// </summary>
public sealed record QueryUserInviteReq : Sys_UserInvite
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <summary>
    ///     是否平面查询
    /// </summary>
    public bool IsPlainQuery { get; init; }
}