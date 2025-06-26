namespace NetAdmin.Domain.Dto.Sys.WalletTrade;

/// <summary>
///     请求：编辑钱包交易
/// </summary>
public record EditWalletTradeReq : CreateWalletTradeReq
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}