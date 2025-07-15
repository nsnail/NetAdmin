namespace NetAdmin.Domain.Dto.Sys.WalletFrozen;

/// <summary>
///     请求：编辑钱包冻结
/// </summary>
public sealed record EditWalletFrozenReq : CreateWalletFrozenReq
{
    /// <inheritdoc cref="Sys_WalletFrozen.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}