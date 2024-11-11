namespace NetAdmin.Domain.Events;

/// <summary>
///     种子数据插入完毕事件
/// </summary>
public sealed record SeedDataInsertedEvent : DataAbstraction, IEventSource
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SeedDataInsertedEvent" /> class.
    /// </summary>
    public SeedDataInsertedEvent(int insertedCount, bool isConsumOnce = false)
    {
        IsConsumOnce  = isConsumOnce;
        InsertedCount = insertedCount;
        CreatedTime   = DateTime.Now;
        EventId       = nameof(SeedDataInsertedEvent);
    }

    /// <inheritdoc />
    public DateTime CreatedTime { get; }

    /// <inheritdoc />
    public string EventId { get; }

    /// <inheritdoc />
    public bool IsConsumOnce { get; }

    /// <inheritdoc />
    public CancellationToken CancellationToken { get; init; }

    /// <summary>
    ///     插入数量
    /// </summary>
    public int InsertedCount { get; set; }

    /// <inheritdoc />
    public object Payload { get; init; }
}