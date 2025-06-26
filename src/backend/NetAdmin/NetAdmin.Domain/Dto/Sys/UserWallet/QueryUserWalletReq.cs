namespace NetAdmin.Domain.Dto.Sys.UserWallet;

/// <summary>
///     请求：查询用户钱包
/// </summary>
public sealed record QueryUserWalletReq : Sys_UserWallet
{
    /// <summary>
    ///     部门编号
    /// </summary>
    public long DeptId { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <summary>
    ///     角色编号
    /// </summary>
    public long RoleId { get; init; }
}