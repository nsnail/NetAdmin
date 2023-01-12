using System.Globalization;
using FreeSql.Aop;
using NSExt.Extensions;

namespace NetAdmin.Api.Events.Sources;

/// <summary>
///     Sql命令执行前事件
/// </summary>
public class SqlCommandBeforeEvent : SqlCommandEvent
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
        return string.Format(CultureInfo.InvariantCulture, "{0}: {1}", Id, Sql);
    }
}