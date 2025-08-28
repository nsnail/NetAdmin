namespace NetAdmin.Domain.Events;

/// <summary>
///     种子数据插入完毕事件
/// </summary>
public sealed record SeedDataInsertedEvent : EventData<int>
{
    /// <inheritdoc />
    public SeedDataInsertedEvent(int payLoad)
        : base(payLoad) {
    }
}