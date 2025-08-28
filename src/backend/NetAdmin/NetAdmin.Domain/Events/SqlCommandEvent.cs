namespace NetAdmin.Domain.Events;

/// <summary>
///     Sql命令事件
/// </summary>
public abstract record SqlCommandEvent : EventData<string>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SqlCommandEvent" /> class.
    /// </summary>
    protected SqlCommandEvent(
        string payLoad
        , Guid identifier
    )
        : base(payLoad) {
        Identifier = identifier;
    }

    /// <summary>
    ///     标识符缩写
    /// </summary>
    protected string Id => Identifier.ToString()[..8].ToUpperInvariant();

    /// <summary>
    ///     标识符，可将 CommandBefore 与 CommandAfter 进行匹配
    /// </summary>
    protected Guid Identifier { get; init; }
}