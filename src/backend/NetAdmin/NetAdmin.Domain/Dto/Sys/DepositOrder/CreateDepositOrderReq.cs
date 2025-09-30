using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.DepositOrder;

/// <summary>
///     请求：创建充值订单
/// </summary>
public record CreateDepositOrderReq : Sys_DepositOrder
{
    /// <inheritdoc cref="Sys_DepositOrder.DepositOrderStatus" />
    public override DepositOrderStatues DepositOrderStatus { get; init; } = DepositOrderStatues.WaitingForPayment;

    /// <inheritdoc cref="Sys_DepositOrder.DepositPoint" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Range(100, int.MaxValue)]
    public override long DepositPoint { get; init; }

    /// <inheritdoc cref="Sys_DepositOrder.PaymentMode" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [EnumDataType(typeof(PaymentModes))]
    public override PaymentModes PaymentMode { get; init; }

    /// <inheritdoc cref="Sys_DepositOrder.ToPointRate" />
    public override int ToPointRate { get; init; }

    /// <inheritdoc />
    protected override IEnumerable<ValidationResult> ValidateInternal(ValidationContext validationContext) {
        if (PaymentMode != PaymentModes.USDT) {
            yield return new ValidationResult(Ln.支付方式不正确, [nameof(PaymentMode)]);
        }

        yield return ValidationResult.Success;
    }
}