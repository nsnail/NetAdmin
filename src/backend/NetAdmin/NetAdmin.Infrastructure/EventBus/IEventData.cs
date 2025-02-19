namespace NetAdmin.Infrastructure.EventBus;

/// <summary>
///     事件数据接口
/// </summary>
public interface IEventData<T>
{
    /// <summary>
    ///     事件发生时间
    /// </summary>
    DateTime CreatedTime { get; init; }

    /// <summary>
    ///     负载
    /// </summary>
    T PayLoad { get; init; }
}