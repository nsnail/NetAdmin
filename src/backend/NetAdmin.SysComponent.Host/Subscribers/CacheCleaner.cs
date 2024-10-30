using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.Events.Sys;

namespace NetAdmin.SysComponent.Host.Subscribers;

/// <summary>
///     缓存清理器
/// </summary>
public sealed class CacheCleaner : IEventSubscriber
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CacheCleaner" /> class.
    /// </summary>
    public CacheCleaner() { }

    /// <summary>
    ///     用户缓存清理
    /// </summary>
    [EventSubscribe(nameof(UserUpdatedEvent))]
    public Task RemoveUserInfoAsync(EventHandlerExecutingContext context)
    {
        if (context.Source is not UserUpdatedEvent userUpdatedEvent) {
            return Task.CompletedTask;
        }

        var cache = App.GetService<IUserCache>();
        cache.Service.UserToken = ContextUserToken.Create(userUpdatedEvent.Data);
        return cache.RemoveUserInfoAsync();
    }
}