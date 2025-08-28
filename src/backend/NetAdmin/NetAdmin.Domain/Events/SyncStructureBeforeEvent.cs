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
        : base(null, e.Identifier) {
        EntityTypes = e.EntityTypes;
    }

    /// <summary>
    ///     实体类型
    /// </summary>
    protected Type[] EntityTypes { get; }

    /// <inheritdoc />
    public override string ToString() {
        return string.Format(CultureInfo.InvariantCulture, "{0}: {1}", Id, Ln.数据库同步开始);
    }
}