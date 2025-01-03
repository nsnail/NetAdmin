using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.Events.Sys;

namespace NetAdmin.SysComponent.Host.Subscribers;

/// <summary>
///     缓存清理器
/// </summary>
public sealed class CacheCleaner : IEventSubscriber
{
    /// <summary>
    ///     用户缓存清理
    /// </summary>
    [EventSubscribe]
    #pragma warning disable CA1822
    public Task RemoveUserInfoAsync(UserUpdatedEvent @event)
        #pragma warning restore CA1822
    {
        var cache = App.GetService<IUserCache>();
        cache.Service.UserToken = ContextUserToken.Create(@event.PayLoad.Id, @event.PayLoad.Token, @event.PayLoad.UserName, @event.PayLoad.DeptId);
        return cache.RemoveUserInfoAsync();
    }
}