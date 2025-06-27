namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     充值订单状态
/// </summary>
[Export]
public enum DepositOrderStatues
{
    /// <summary>
    ///     等待支付
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.等待支付))]
    WaitingForPayment = 1

   ,

    /// <summary>
    ///     到账确认中
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.到账确认中))]
    PaymentConfirming = 2

   ,

    /// <summary>
    ///     充值成功
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.充值成功))]
    Succeeded = 3

   ,

    /// <summary>
    ///     支付超时
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.支付超时))]
    PaymentTimeout = 4
}