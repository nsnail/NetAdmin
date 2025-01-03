namespace NetAdmin.Infrastructure.EventBus;

/// <summary>
///     事件处理程序特性
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class EventSubscribeAttribute : Attribute;