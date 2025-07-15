namespace NetAdmin.Domain.Dto.Sys.WalletFrozen;

/// <summary>
///     请求：查询钱包冻结
/// </summary>
public sealed record QueryWalletFrozenReq : Sys_WalletFrozen
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}