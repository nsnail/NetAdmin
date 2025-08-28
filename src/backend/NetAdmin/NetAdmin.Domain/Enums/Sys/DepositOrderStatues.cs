namespace NetAdmin.Domain.Enums.Sys;

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
    [EnumDecoration(nameof(Indicates.Info))]
    WaitingForPayment = 1

    ,

    /// <summary>
    ///     到账确认中
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.到账确认中))]
    [EnumDecoration(nameof(Indicates.Warning), true)]
    PaymentConfirming = 2

    ,

    /// <summary>
    ///     充值成功
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.充值成功))]
    [EnumDecoration(nameof(Indicates.Success))]
    Succeeded = 3

    ,

    /// <summary>
    ///     支付超时
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.支付超时))]
    [EnumDecoration(nameof(Indicates.Danger))]
    PaymentTimeout = 4
}