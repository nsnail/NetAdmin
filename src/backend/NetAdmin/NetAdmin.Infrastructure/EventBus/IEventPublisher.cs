namespace NetAdmin.Infrastructure.EventBus;

/// <summary>
///     事件发布器接口
/// </summary>
public interface IEventPublisher
{
    /// <summary>
    ///     发布一条消息
    /// </summary>
    Task PublishAsync<T>(IEventData<T> eventData);
}