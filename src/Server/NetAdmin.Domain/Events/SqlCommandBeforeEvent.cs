namespace NetAdmin.Domain.Events;

/// <summary>
///     Sql命令执行前事件
/// </summary>
public record SqlCommandBeforeEvent : SqlCommandEvent
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SqlCommandBeforeEvent" /> class.
    /// </summary>
    public SqlCommandBeforeEvent(CommandBeforeEventArgs e)
    {
        Identifier  = e.Identifier;
        Sql         = e.Command.ParameterFormat().RemoveWrapped();
        EventId     = nameof(SqlCommandBeforeEvent);
        CreatedTime = DateTime.Now;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return string.Format(CultureInfo.InvariantCulture, "SQL-{0}: {1}", Id, Sql);
    }
}