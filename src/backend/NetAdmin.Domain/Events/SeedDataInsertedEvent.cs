namespace NetAdmin.Domain.Events;

/// <summary>
///     种子数据插入完毕事件
/// </summary>
public sealed record SeedDataInsertedEvent : DataAbstraction, IEventSource
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SeedDataInsertedEvent" /> class.
    /// </summary>
    public SeedDataInsertedEvent(int insertedCount)
    {
        InsertedCount = insertedCount;
        EventId       = nameof(SeedDataInsertedEvent);
    }

    /// <inheritdoc />
    public CancellationToken CancellationToken { get; }

    /// <inheritdoc />
    public DateTime CreatedTime { get; }

    /// <inheritdoc />
    public string EventId { get; }

    /// <inheritdoc />
    public bool IsConsumOnce { get; }

    /// <inheritdoc />
    public object Payload { get; }

    /// <summary>
    ///     插入数量
    /// </summary>
    public int InsertedCount { get; set; }
}