namespace NetAdmin.Domain.Dto.Sys.DepositOrder;

/// <summary>
///     请求：支付确认
/// </summary>
public record PayConfirmReq : Sys_DepositOrder
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}