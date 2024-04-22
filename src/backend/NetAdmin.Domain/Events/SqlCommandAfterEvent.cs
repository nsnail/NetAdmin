namespace NetAdmin.Domain.Events;

/// <summary>
///     Sql命令执行后事件
/// </summary>
public sealed record SqlCommandAfterEvent : SqlCommandBeforeEvent
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SqlCommandAfterEvent" /> class.
    /// </summary>
    public SqlCommandAfterEvent(CommandAfterEventArgs e) //
        : base(e)
    {
        ElapsedMicroseconds = (long)((double)e.ElapsedTicks / Stopwatch.Frequency * 1_000_000);
        EventId             = nameof(SqlCommandAfterEvent);
    }

    /// <summary>
    ///     耗时（单位：微秒）
    /// </summary>
    /// de
    private long ElapsedMicroseconds { get; }

    /// <inheritdoc />
    public override string ToString()
    {
        return string.Format(CultureInfo.InvariantCulture, "SQL-{0}: {2} ms {1}", Id
                           , Sql?.Sub(0, Numbers.MAX_LIMIT_PRINT_LEN_SQL), ElapsedMicroseconds / 1000);
    }
}