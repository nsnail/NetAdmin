namespace NetAdmin.Host.BackgroundRunning;

/// <summary>
///     工作基类
/// </summary>
public abstract class WorkBase<TLogger>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="WorkBase{TLogger}" /> class.
    /// </summary>
    protected WorkBase()
    {
        ServiceProvider = App.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider;
        UowManager      = ServiceProvider.GetService<UnitOfWorkManager>();
        Logger          = ServiceProvider.GetService<ILogger<TLogger>>();
    }

    /// <summary>
    ///     日志记录器
    /// </summary>
    protected ILogger<TLogger> Logger { get; }

    /// <summary>
    ///     服务提供器
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <summary>
    ///     事务单元管理器
    /// </summary>
    protected UnitOfWorkManager UowManager { get; }

    /// <summary>
    ///     通用工作流
    /// </summary>
    protected abstract ValueTask WorkflowAsync(CancellationToken cancelToken);
}