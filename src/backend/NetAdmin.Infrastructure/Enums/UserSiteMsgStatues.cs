namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     站内信状态
/// </summary>
[Export]
public enum UserSiteMsgStatues
{
    /// <summary>
    ///     未读
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.未读))]
    Unread = 1

   ,

    /// <summary>
    ///     已读
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.已读))]
    Read = 2

   ,

    /// <summary>
    ///     删除
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.删除))]
    Deleted = 3
}