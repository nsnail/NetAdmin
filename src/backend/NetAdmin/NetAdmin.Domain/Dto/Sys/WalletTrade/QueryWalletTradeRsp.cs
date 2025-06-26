using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.Dto.Sys.WalletTrade;

/// <summary>
///     响应：查询钱包交易
/// </summary>
public record QueryWalletTradeRsp : Sys_WalletTrade
{
    /// <inheritdoc cref="Sys_WalletTrade.Amount" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Amount { get; init; }

    /// <inheritdoc cref="Sys_WalletTrade.BalanceBefore" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long BalanceBefore { get; init; }

    /// <inheritdoc cref="Sys_WalletTrade.BusinessOrderNumber" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? BusinessOrderNumber { get; init; }

    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="IFieldCreatedUser.CreatedUserId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? CreatedUserId { get; init; }

    /// <inheritdoc cref="IFieldCreatedUser.CreatedUserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string CreatedUserName { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="Sys_WalletTrade.Owner" />
    [CsvIgnore]
    public new virtual QueryUserRsp Owner { get; init; }

    /// <inheritdoc cref="IFieldOwner.OwnerDeptId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? OwnerDeptId { get; init; }

    /// <inheritdoc cref="IFieldOwner.OwnerId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? OwnerId { get; init; }

    /// <inheritdoc cref="IFieldSummary.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Summary { get; init; }

    /// <inheritdoc cref="Sys_WalletTrade.TradeDirection" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override TradeDirections TradeDirection { get; init; }

    /// <inheritdoc cref="Sys_WalletTrade.TradeType" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override TradeTypes TradeType { get; init; }
}