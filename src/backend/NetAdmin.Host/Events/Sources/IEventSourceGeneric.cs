using Furion.EventBus;

namespace NetAdmin.Host.Events.Sources;

/// <summary>
///     泛型事件源接口
/// </summary>
public interface IEventSourceGeneric<out T> : IEventSource
{
    /// <summary>
    ///     事件承载（携带）数据
    /// </summary>
    T Data { get; }
}