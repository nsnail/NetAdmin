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
        : base(e.Command.ParameterFormat().RemoveWrapped(), e.Identifier) {
    }

    /// <inheritdoc />
    public override string ToString() {
        return string.Format(CultureInfo.InvariantCulture, "SQL-{0}: Executing...", Id);
    }
}