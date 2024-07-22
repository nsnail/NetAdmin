using RedLockNet;

namespace NetAdmin.Host.BackgroundRunning;

/// <summary>
///     工作基类
/// </summary>
public abstract class WorkBase<TLogger>
{
    private readonly RedLocker _redLocker;

    /// <summary>
    ///     Initializes a new instance of the <see cref="WorkBase{TLogger}" /> class.
    /// </summary>
    protected WorkBase()
    {
        ServiceProvider = App.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider;
        UowManager      = ServiceProvider.GetService<UnitOfWorkManager>();
        Logger          = ServiceProvider.GetService<ILogger<TLogger>>();
        _redLocker      = ServiceProvider.GetService<RedLocker>();
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
    protected abstract ValueTask WorkflowAsync( //

        // ReSharper disable once UnusedParameter.Global
        #pragma warning disable SA1114
        CancellationToken cancelToken);
    #pragma warning restore SA1114

    /// <summary>
    ///     通用工作流
    /// </summary>
    /// <exception cref="NetAdminGetLockerException">加锁失败异常</exception>
    protected async ValueTask WorkflowAsync(bool singleInstance, CancellationToken cancelToken)
    {
        if (singleInstance) {
            // 加锁
            await using var redLock = await GetLockerAsync(GetType().FullName).ConfigureAwait(false);
            if (!redLock.IsAcquired) {
                throw new NetAdminGetLockerException();
            }

            await WorkflowAsync(cancelToken).ConfigureAwait(false);
            return;
        }

        await WorkflowAsync(cancelToken).ConfigureAwait(false);
    }

    /// <summary>
    ///     获取锁
    /// </summary>
    private Task<IRedLock> GetLockerAsync(string lockId)
    {
        return _redLocker.RedLockFactory.CreateLockAsync(lockId, TimeSpan.FromSeconds(Numbers.SECS_RED_LOCK_EXPIRY)
                                                       , TimeSpan.FromSeconds(Numbers.SECS_RED_LOCK_WAIT)
                                                       , TimeSpan.FromSeconds(Numbers.SECS_RED_LOCK_RETRY));
    }
}