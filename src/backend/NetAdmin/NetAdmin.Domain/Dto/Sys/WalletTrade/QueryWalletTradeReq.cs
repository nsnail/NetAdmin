using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.WalletTrade;

/// <summary>
///     请求：查询钱包交易
/// </summary>
public sealed record QueryWalletTradeReq : Sys_WalletTrade
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}