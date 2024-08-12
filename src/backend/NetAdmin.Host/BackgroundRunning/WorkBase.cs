using StackExchange.Redis;

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
            var             lockName    = GetType().FullName;
            await using var redisLocker = await GetLockerAsync(lockName).ConfigureAwait(false);

            await WorkflowAsync(cancelToken).ConfigureAwait(false);
            return;
        }

        await WorkflowAsync(cancelToken).ConfigureAwait(false);
    }

    /// <summary>
    ///     获取锁
    /// </summary>
    private Task<RedisLocker> GetLockerAsync(string lockId)
    {
        var db = ServiceProvider.GetService<IConnectionMultiplexer>()
                                .GetDatabase(ServiceProvider.GetService<IOptions<RedisOptions>>()
                                                            .Value.Instances
                                                            .First(x => x.Name == Chars.FLG_REDIS_INSTANCE_DATA_CACHE)
                                                            .Database);
        return RedisLocker.GetLockerAsync(db, lockId, TimeSpan.FromSeconds(Numbers.SECS_REDIS_LOCK_EXPIRY)
                                        , Numbers.MAX_LIMIT_RETRY_CNT_REDIS_LOCK
                                        , TimeSpan.FromSeconds(Numbers.SECS_REDIS_LOCK_RETRY_DELAY));
    }
}