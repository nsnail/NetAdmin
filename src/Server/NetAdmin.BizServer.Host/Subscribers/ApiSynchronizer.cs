using NetAdmin.Domain.Events;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.BizServer.Host.Subscribers;

/// <summary>
///     Api接口同步器
/// </summary>
public sealed class ApiSynchronizer : IEventSubscriber
{
    private readonly ILogger<ApiSynchronizer> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ApiSynchronizer" /> class.
    /// </summary>
    public ApiSynchronizer(ILogger<ApiSynchronizer> logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     同步Api接口
    /// </summary>
    [EventSubscribe(nameof(SyncStructureAfterEvent))]
    public async Task SyncApiAsync(EventHandlerExecutingContext _)
    {
        var logService = App.GetService<IApiService>();
        await logService.SyncAsync();
        _logger.Info($"{nameof(IApiService)}.{nameof(IApiService.SyncAsync)} {Ln.Completed}");
    }
}