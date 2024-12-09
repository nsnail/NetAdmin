namespace NetAdmin.Domain.Events;

/// <summary>
///     Sql命令事件
/// </summary>
public abstract record SqlCommandEvent : DataAbstraction, IEventSource
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SqlCommandEvent" /> class.
    /// </summary>
    protected SqlCommandEvent(bool isConsumeOnce = false)
    {
        IsConsumeOnce = isConsumeOnce;
    }

    /// <inheritdoc />
    public bool IsConsumeOnce { get; }

    /// <inheritdoc />
    public CancellationToken CancellationToken { get; init; }

    /// <inheritdoc />
    public DateTime CreatedTime { get; protected init; }

    /// <inheritdoc />
    public string EventId { get; protected init; }

    /// <inheritdoc />
    public object Payload { get; init; }

    /// <summary>
    ///     标识符缩写
    /// </summary>
    protected string Id => Identifier.ToString()[..8].ToUpperInvariant();

    /// <summary>
    ///     标识符，可将 CommandBefore 与 CommandAfter 进行匹配
    /// </summary>
    protected Guid Identifier { get; init; }

    /// <summary>
    ///     关联的Sql语句
    /// </summary>
    protected string Sql { get; init; }
}