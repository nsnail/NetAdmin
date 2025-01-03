using NetAdmin.Domain.Events;

namespace NetAdmin.SysComponent.Host.Subscribers;

/// <summary>
///     Api接口同步器
/// </summary>
public sealed class ApiSynchronizer(ILogger<ApiSynchronizer> logger) : IEventSubscriber
{
    /// <summary>
    ///     同步Api接口
    /// </summary>
    [EventSubscribe]
    public async Task SyncApiAsync(SyncStructureAfterEvent _)
    {
        var logService = App.GetService<IApiService>();
        await logService.SyncAsync().ConfigureAwait(false);
        logger.Info($"{nameof(IApiService)}.{nameof(IApiService.SyncAsync)} {Ln.已处理完毕}");
    }
}