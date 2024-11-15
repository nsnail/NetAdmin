namespace NetAdmin.Domain.Enums.Sys;

/// <summary>
///     验证码状态
/// </summary>
[Export]
public enum VerifyCodeStatues
{
    /// <summary>
    ///     等待发送
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.等待发送))]
    Waiting = 1

   ,

    /// <summary>
    ///     已发送
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.已发送))]
    Sent = 2

   ,

    /// <summary>
    ///     发送失败
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.发送失败))]
    Failed = 3

   ,

    /// <summary>
    ///     已校验
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.已校验))]
    Verified = 4
}