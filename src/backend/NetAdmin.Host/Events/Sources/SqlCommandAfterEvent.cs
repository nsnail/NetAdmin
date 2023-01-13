using System.Globalization;
using FreeSql.Aop;

namespace NetAdmin.Host.Events.Sources;

/// <summary>
///     Sql命令执行后事件
/// </summary>
public class SqlCommandAfterEvent : SqlCommandBeforeEvent
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SqlCommandAfterEvent" /> class.
    /// </summary>
    public SqlCommandAfterEvent(CommandAfterEventArgs e) //
        : base(e)
    {
        ElapsedMilliseconds = e.ElapsedMilliseconds;
    }

    /// <summary>
    ///     耗时（单位：毫秒）
    /// </summary>
    public long ElapsedMilliseconds { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return string.Format(CultureInfo.InvariantCulture, "SQL-{0}: {1} ms", Id, ElapsedMilliseconds);
    }
}