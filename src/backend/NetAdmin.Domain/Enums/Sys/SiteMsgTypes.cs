namespace NetAdmin.Domain.Enums.Sys;

/// <summary>
///     站内信类型
/// </summary>
[Export]
public enum SiteMsgTypes
{
    /// <summary>
    ///     私信
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.私信))]
    Private = 1

   ,

    /// <summary>
    ///     公告
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.公告))]
    Public = 2
}