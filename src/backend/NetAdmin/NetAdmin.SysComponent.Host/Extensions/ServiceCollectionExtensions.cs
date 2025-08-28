using Cronos;
using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.Events;
using NetAdmin.Host.Filters;
using NetAdmin.Host.Middlewares;
using NetAdmin.Infrastructure.Configuration.Dependency;
using NetAdmin.Infrastructure.Schedule;
using NetAdmin.SysComponent.Host.Utils;
using FreeSqlBuilder = NetAdmin.Infrastructure.Utils.FreeSqlBuilder;

namespace NetAdmin.SysComponent.Host.Extensions;

/// <summary>
///     ServiceCollection 扩展方法
/// </summary>
[SuppressSniffer]
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     添加上下文用户信息
    /// </summary>
    public static IServiceCollection AddContextUserInfo(this IServiceCollection me) {
        return me.AddScoped(typeof(ContextUserInfo), _ => ContextUserInfo.Create());
    }

    /// <summary>
    ///     添加 freeSql orm工具
    /// </summary>
    public static IServiceCollection AddFreeSql(
        this IServiceCollection me
        , FreeSqlInitMethods initMethods = FreeSqlInitMethods.None
        , Action<IFreeSql> freeSqlConfig = null
    ) {
        // // 非调试模式下禁止同步数据库
        // #if !DEBUG
        // initOptions = FreeSqlInitOptions.None;
        // #endif
        var dbOptions = App.GetOptions<DatabaseOptions>();
        var eventPublisher = App.GetService<IEventPublisher>();

        var fSql = new FreeSqlBuilder(dbOptions).Build(initMethods, count => eventPublisher.PublishAsync(new SeedDataInsertedEvent(count)));
        _ = me.AddSingleton(fSql);

        fSql.Aop.AuditValue += SqlAuditor.DataAuditHandler; // Insert/Update自动值处理

        #pragma warning disable VSTHRD110

        // AOP事件发布（异步）
        fSql.Aop.CommandBefore += (
            _
            , e
        ) => eventPublisher.PublishAsync(new SqlCommandBeforeEvent(e)); // 增删查改，执行命令之前触发
        fSql.Aop.CommandAfter += (
            _
            , e
        ) => eventPublisher.PublishAsync(new SqlCommandAfterEvent(e)); // 增删查改，执行命令完成后触发

        fSql.Aop.SyncStructureBefore += (
            _
            , e
        ) => eventPublisher.PublishAsync(new SyncStructureBeforeEvent(e)); // CodeFirst迁移，执行之前触发

        fSql.Aop.SyncStructureAfter += (
            _
            , e
        ) => eventPublisher.PublishAsync(new SyncStructureAfterEvent(e)); // CodeFirst迁移，执行完成触发
        #pragma warning restore VSTHRD110

        // 全局过滤器设置
        freeSqlConfig?.Invoke(fSql);

        return me
            .AddScoped<UnitOfWorkManager>() // 注入工作单元管理器
            .AddFreeRepository(null, App.Assemblies.ToArray()) // 批量注入 Repository
            .AddMvcFilter<TransactionInterceptor>(); // 注入事务拦截器
    }

    /// <summary>
    ///     添加定时任务
    /// </summary>
    public static IServiceCollection AddSchedules(
        this IServiceCollection me
        , bool force = false
    ) {
        if (!App.WebHostEnvironment.IsProduction() && !force) {
            return me;
        }

        var jobTypes = App
            .EffectiveTypes.Where(x => typeof(IJob).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract && x.IsDefined(typeof(JobConfigAttribute)))
            .ToDictionary(x => x, x => x.GetCustomAttribute<JobConfigAttribute>());
        var runOnStartJobTypes = jobTypes.Where(x => x.Value.RunOnStart);
        RunJob(runOnStartJobTypes);
        _ = Task.Run(LoopTaskAsync);
        return me;

        #pragma warning disable S2190
        async Task LoopTaskAsync()
            #pragma warning restore S2190
        {
            while (true) {
                await Task.Delay(1000).ConfigureAwait(false);
                if (SafetyShopHostMiddleware.IsShutdown) {
                    Console.WriteLine(Ln.此节点已下线);
                }
                else {
                    RunJob(jobTypes.Where(Filter));
                }
            }

            bool Filter(KeyValuePair<Type, JobConfigAttribute> x) {
                return !x.Value.TriggerCron.NullOrEmpty()
                       && CronExpression
                           .Parse(x.Value.TriggerCron, CronFormat.IncludeSeconds)
                           .GetNextOccurrence(x.Value.LastExecutionTime ?? DateTime.UtcNow.AddDays(-1), TimeZoneInfo.Local)
                           ?.ToLocalTime()
                       <= DateTime.Now;
            }

            // ReSharper disable once FunctionNeverReturns
        }
    }

    /// <summary>
    ///     添加 TronScan 客户端
    /// </summary>
    public static IServiceCollection AddTronScanClient(this IServiceCollection me) {
        _ = me.AddHttpClient(nameof(TronScanOptions), ConfigClient<TronScanOptions>);
        return me;
    }

    private static void ConfigClient<T>(HttpClient client)
        where T : ApiClientOptions, new() {
        ConfigClient<T>(client, Numbers.SECS_TIMEOUT_HTTP_CLIENT);
    }

    private static void ConfigClient<T>(
        HttpClient client
        , int timeoutSecs
    )
        where T : ApiClientOptions, new() {
        var options = App.GetOptions<T>();

        client.Timeout = TimeSpan.FromSeconds(options.TimeoutSecs > 0 ? options.TimeoutSecs : timeoutSecs);
        client.DefaultRequestHeaders.Add("User-Agent", nameof(NetAdmin));
        client.BaseAddress = new Uri($"{options.Gateway}/");
    }

    private static void RunJob(IEnumerable<KeyValuePair<Type, JobConfigAttribute>> jobTypes) {
        foreach (var job in jobTypes) {
            try {
                _ = typeof(IJob).GetMethod(nameof(IJob.ExecuteAsync))!.Invoke(Activator.CreateInstance(job.Key), [CancellationToken.None]);
                job.Value.LastExecutionTime = DateTime.UtcNow;
            }
            catch (Exception ex) {
                LogHelper.Get<IServiceCollection>().Error(ex);
            }
        }
    }
}