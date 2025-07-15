using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.Dto.Sys.UserWallet;

/// <summary>
///     响应：查询用户钱包
/// </summary>
public record QueryUserWalletRsp : Sys_UserWallet
{
    /// <inheritdoc cref="Sys_UserWallet.AvailableBalance" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long AvailableBalance { get; init; }

    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="Sys_UserWallet.FrozenBalance" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long FrozenBalance { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="IFieldModifiedTime.ModifiedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override DateTime? ModifiedTime { get; init; }

    /// <inheritdoc cref="Sys_UserWallet.Owner" />
    public new virtual QueryUserRsp Owner { get; init; }

    /// <inheritdoc cref="IFieldOwner.OwnerDeptId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? OwnerDeptId { get; init; }

    /// <inheritdoc cref="IFieldOwner.OwnerId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? OwnerId { get; init; }

    /// <inheritdoc cref="Sys_UserWallet.TotalExpenditure" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long TotalExpenditure { get; init; }

    /// <inheritdoc cref="Sys_UserWallet.TotalIncome" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long TotalIncome { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}