using NetAdmin.Domain.Events;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Subscribers;

/// <summary>
///     Api接口同步器
/// </summary>
public sealed class ApiSynchronizer(ILogger<ApiSynchronizer> logger) : IEventSubscriber
{
    /// <summary>
    ///     同步Api接口
    /// </summary>
    [EventSubscribe(nameof(SyncStructureAfterEvent))]
    public async Task SyncApiAsync(EventHandlerExecutingContext _)
    {
        var logService = App.GetService<IApiService>();
        await logService.SyncAsync();
        logger.Info($"{nameof(IApiService)}.{nameof(IApiService.SyncAsync)} {Ln.已完成}");
    }
}