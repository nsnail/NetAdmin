using Furion.EventBus;
using NetAdmin.Host.Events.Sources;
using NSExt.Extensions;

namespace NetAdmin.Host.Events.Subscribers;

/// <summary>
///     Sql性能分析
/// </summary>
public class SqlProfiler : IEventSubscriber
{
    private readonly ILogger<SqlProfiler> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SqlProfiler" /> class.
    /// </summary>
    public SqlProfiler(ILogger<SqlProfiler> logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     Sql命令执行后
    /// </summary>
    [EventSubscribe(nameof(SqlCommandAfterEvent))]
    public Task CommandAfter(EventHandlerExecutingContext context)
    {
        var source = context.Source as SqlCommandAfterEvent;
        _logger.Info(source);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Sql命令执行前
    /// </summary>
    [EventSubscribe(nameof(SqlCommandBeforeEvent))]
    public Task CommandBefore(EventHandlerExecutingContext context)
    {
        var source = context.Source as SqlCommandBeforeEvent;
        _logger.Info(source);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     同步数据库结构之后
    /// </summary>
    [EventSubscribe(nameof(SyncStructureAfterEvent))]
    public Task SyncStructureAfter(EventHandlerExecutingContext context)
    {
        var source = context.Source as SyncStructureAfterEvent;
        _logger.Info(source);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     同步数据库结构之前
    /// </summary>
    [EventSubscribe(nameof(SyncStructureBeforeEvent))]
    public Task SyncStructureBefore(EventHandlerExecutingContext context)
    {
        var source = context.Source as SyncStructureBeforeEvent;
        _logger.Info(source);
        return Task.CompletedTask;
    }
}