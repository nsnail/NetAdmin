namespace NetAdmin.Domain.Events;

/// <summary>
///     同步数据库结构之后事件
/// </summary>
public sealed record SyncStructureAfterEvent : SyncStructureBeforeEvent
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SyncStructureAfterEvent" /> class.
    /// </summary>
    public SyncStructureAfterEvent(SyncStructureBeforeEventArgs e) //
        : base(e)
    {
        EventId = nameof(SyncStructureAfterEvent);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return string.Format(CultureInfo.InvariantCulture, "{0}: {1}: {2}", Id, Ln.数据库结构同步完成
                           , string.Join(',', EntityTypes.Select(x => x.Name)));
    }
}