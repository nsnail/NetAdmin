namespace NetAdmin.Domain.Dto.Sys.DepositOrder;

/// <summary>
///     请求：编辑充值订单
/// </summary>
public record EditDepositOrderReq : CreateDepositOrderReq
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}