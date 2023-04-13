namespace NetAdmin.Domain.Enums.Res;

/// <summary>
///     在线状态
/// </summary>
[Export]
public enum OnlineStatues
{
    /// <summary>
    ///     在线
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Online))]
    Online = 10

   ,

    /// <summary>
    ///     离线
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Offline))]
    Offline = 11

   ,

    /// <summary>
    ///     封禁
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Limited))]
    Limited = 12
}