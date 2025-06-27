namespace NetAdmin.Domain.Dto.Sys.DepositOrder;

/// <summary>
///     请求：查询充值订单
/// </summary>
public sealed record QueryDepositOrderReq : Sys_DepositOrder
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}