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
    [EventSubscribe]
    public Task CommandAfterAsync(SqlCommandAfterEvent @event) {
        logger.Info(@event);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Sql命令执行前
    /// </summary>
    [EventSubscribe]
    public Task CommandBeforeAsync(SqlCommandBeforeEvent @event) {
        logger.Debug(@event);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     种子数据插入完毕
    /// </summary>
    [EventSubscribe]
    public Task SeedDataInsertedEventAsync(SeedDataInsertedEvent @event) {
        logger.Info(@event);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     同步数据库结构之后
    /// </summary>
    [EventSubscribe]
    public Task SyncStructureAfterAsync(SyncStructureAfterEvent @event) {
        logger.Info(@event);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     同步数据库结构之前
    /// </summary>
    [EventSubscribe]
    public Task SyncStructureBeforeAsync(SyncStructureBeforeEvent @event) {
        logger.Info(@event);
        return Task.CompletedTask;
    }
}