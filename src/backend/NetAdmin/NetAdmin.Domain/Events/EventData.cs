namespace NetAdmin.Domain.Events;

/// <summary>
///     事件数据
/// </summary>
public abstract record EventData<T> : DataAbstraction, IEventData<T>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EventData{T}" /> class.
    /// </summary>
    protected EventData(T payLoad) {
        PayLoad = payLoad;
    }

    /// <inheritdoc />
    public DateTime CreatedTime { get; init; } = DateTime.Now;

    /// <inheritdoc />
    public T PayLoad { get; init; }
}