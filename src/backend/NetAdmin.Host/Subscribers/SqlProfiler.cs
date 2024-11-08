using NetAdmin.Domain.Events;

namespace NetAdmin.Host.Subscribers;

/// <summary>
///     Sql性能分析
/// </summary>
public sealed class SqlProfiler(ILogger<SqlProfiler> logger) : IEventSubscriber
{
    /// <summary>
    ///     Sql命令执行后
    /// </summary>
    [EventSubscribe(nameof(SqlCommandAfterEvent))]
    public Task CommandAfterAsync(EventHandlerExecutingContext context)
    {
        var source = context.Source as SqlCommandAfterEvent;
        logger.Info(source);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Sql命令执行前
    /// </summary>
    [EventSubscribe(nameof(SqlCommandBeforeEvent))]
    public Task CommandBeforeAsync(EventHandlerExecutingContext context)
    {
        var source = context.Source as SqlCommandBeforeEvent;
        logger.Debug(source);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     种子数据插入完毕
    /// </summary>
    [EventSubscribe(nameof(SeedDataInsertedEvent))]
    public Task SeedDataInsertedEventAsync(EventHandlerExecutingContext context)
    {
        var source = context.Source as SeedDataInsertedEvent;
        logger.Info(source);
        if (App.WebHostEnvironment.IsDevelopment()) {
            Environment.Exit(0);
        }

        return Task.CompletedTask;
    }

    /// <summary>
    ///     同步数据库结构之后
    /// </summary>
    [EventSubscribe(nameof(SyncStructureAfterEvent))]
    public Task SyncStructureAfterAsync(EventHandlerExecutingContext context)
    {
        var source = context.Source as SyncStructureAfterEvent;
        logger.Info(source);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     同步数据库结构之前
    /// </summary>
    [EventSubscribe(nameof(SyncStructureBeforeEvent))]
    public Task SyncStructureBeforeAsync(EventHandlerExecutingContext context)
    {
        var source = context.Source as SyncStructureBeforeEvent;
        logger.Info(source);
        return Task.CompletedTask;
    }
}