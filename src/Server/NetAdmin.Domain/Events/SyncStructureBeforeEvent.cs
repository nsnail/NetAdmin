namespace NetAdmin.Domain.Events;

/// <summary>
///     同步数据库结构之前事件
/// </summary>
public record SyncStructureBeforeEvent : SqlCommandEvent
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SyncStructureBeforeEvent" /> class.
    /// </summary>
    public SyncStructureBeforeEvent(SyncStructureBeforeEventArgs e)
    {
        Identifier  = e.Identifier;
        EventId     = nameof(SyncStructureBeforeEvent);
        CreatedTime = DateTime.Now;
        EntityTypes = e.EntityTypes;
    }

    /// <summary>
    ///     实体类型
    /// </summary>
    public Type[] EntityTypes { get; }

    /// <inheritdoc />
    public override string ToString()
    {
        return string.Format(CultureInfo.InvariantCulture, "{0}: {1}", Id
                           , Ln.Database_structure_synchronization_starts);
    }
}