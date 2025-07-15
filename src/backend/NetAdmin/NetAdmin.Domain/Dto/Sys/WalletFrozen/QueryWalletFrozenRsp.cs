using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.WalletFrozen;

/// <summary>
///     响应：查询钱包冻结
/// </summary>
public record QueryWalletFrozenRsp : Sys_WalletFrozen
{
    /// <inheritdoc cref="Sys_WalletFrozen.Amount" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Amount { get; init; }

    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="Sys_WalletFrozen.FrozenBalanceBefore" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long FrozenBalanceBefore { get; init; }

    /// <inheritdoc cref="Sys_WalletFrozen.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="IFieldModifiedTime.ModifiedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override DateTime? ModifiedTime { get; init; }

    /// <inheritdoc cref="Sys_WalletFrozen.Owner" />
    public new virtual QueryUserRsp Owner { get; init; }

    /// <inheritdoc cref="IFieldOwner.OwnerDeptId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? OwnerDeptId { get; init; }

    /// <inheritdoc cref="IFieldOwner.OwnerId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? OwnerId { get; init; }

    /// <inheritdoc cref="Sys_WalletFrozen.Reason" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override WalletFrozenReasons Reason { get; init; }

    /// <inheritdoc cref="Sys_WalletFrozen.Status" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override WalletFrozenStatues Status { get; init; }

    /// <inheritdoc cref="IFieldSummary.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Summary { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}