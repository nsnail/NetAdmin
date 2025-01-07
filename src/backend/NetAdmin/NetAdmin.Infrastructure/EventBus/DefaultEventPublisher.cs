using System.Threading.Channels;

namespace NetAdmin.Infrastructure.EventBus;

/// <summary>
///     事件发布器默认实现
/// </summary>
public sealed class DefaultEventPublisher : IEventPublisher
{
    private readonly Channel<object> _eventChannel;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DefaultEventPublisher" /> class.
    /// </summary>
    public DefaultEventPublisher()
    {
        _eventChannel = Channel.CreateUnbounded<object>();
        _ = new TaskFactory<Task>().StartNew( //
            async state => {
                var subscribers = (List<MethodInfo>)state;
                await Parallel.ForEachAsync(_eventChannel.Reader.ReadAllAsync(), (msg, __) => {
                                  _ = Parallel.ForEach( //
                                      subscribers.Where(x => x.GetParameters().FirstOrDefault()?.ParameterType == msg.GetType())
                                    , (x, _) => x.Invoke(App.GetService(x.DeclaringType), [msg]));
                                  return ValueTask.CompletedTask;
                              })
                              .ConfigureAwait(false);
            }, App.EffectiveTypes.Where(x => typeof(IEventSubscriber).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract).SelectMany(x => x.GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(y => y.IsDefined(typeof(EventSubscribeAttribute)))).ToList());
    }

    /// <inheritdoc />
    public async Task PublishAsync<T>(IEventData<T> eventData)
    {
        await _eventChannel.Writer.WriteAsync(eventData).ConfigureAwait(false);
    }
}