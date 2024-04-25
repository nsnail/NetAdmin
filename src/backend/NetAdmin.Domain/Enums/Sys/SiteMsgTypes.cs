namespace NetAdmin.Domain.Enums.Sys;

/// <summary>
///     站内信类型
/// </summary>
[Export]
public enum SiteMsgTypes
{
    /// <summary>
    ///     通知
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.通知))]
    Private = 1

   ,

    /// <summary>
    ///     公告
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.公告))]
    Public = 2
}