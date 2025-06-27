namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     支付方式
/// </summary>
[Export]
public enum PaymentModes
{
    /// <summary>
    ///     USDT
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.USDT))]
    USDT = 1

   ,

    /// <summary>
    ///     支付宝
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.支付宝))]
    Alipay = 2

   ,

    /// <summary>
    ///     微信支付
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.微信支付))]
    WeChat = 3
}