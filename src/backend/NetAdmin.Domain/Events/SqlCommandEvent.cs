namespace NetAdmin.Domain.Events;

/// <summary>
///     Sql命令事件
/// </summary>
public record SqlCommandEvent : DataAbstraction, IEventSource
{
    /// <summary>
    ///     标识符缩写
    /// </summary>
    public string Id => Identifier.ToString()[..8].ToUpperInvariant();

    /// <summary>
    ///     取消任务 Token
    /// </summary>
    /// <remarks>
    ///     用于取消本次消息处理
    /// </remarks>
    public CancellationToken CancellationToken { get; init; }

    /// <summary>
    ///     事件创建时间
    /// </summary>
    public DateTime CreatedTime { get; protected init; }

    /// <summary>
    ///     事件 Id
    /// </summary>
    public string EventId { get; protected init; }

    /// <summary>
    ///     标识符，可将 CommandBefore 与 CommandAfter 进行匹配
    /// </summary>
    public Guid Identifier { get; protected init; }

    /// <summary>
    ///     事件承载（携带）数据
    /// </summary>
    public object Payload { get; init; }

    /// <summary>
    ///     关联的Sql语句
    /// </summary>
    public string Sql { get; protected init; }
}