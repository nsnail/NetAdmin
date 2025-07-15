using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.WalletFrozen;

/// <summary>
///     请求：创建钱包冻结
/// </summary>
public record CreateWalletFrozenReq : Sys_WalletFrozen
{
    /// <inheritdoc cref="Sys_WalletFrozen.Amount" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Amount { get; init; }

    /// <inheritdoc cref="Sys_WalletFrozen.OwnerId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? OwnerId { get; init; }

    /// <inheritdoc cref="Sys_WalletFrozen.Reason" />
    [EnumDataType(typeof(WalletFrozenReasons))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override WalletFrozenReasons Reason { get; init; }

    /// <inheritdoc cref="Sys_WalletFrozen.Status" />
    public override WalletFrozenStatues Status { get; init; } = WalletFrozenStatues.Frozen;

    /// <inheritdoc cref="IFieldSummary.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Summary { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}