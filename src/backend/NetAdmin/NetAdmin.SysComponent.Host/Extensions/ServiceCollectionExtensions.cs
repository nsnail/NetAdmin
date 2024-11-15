using Gurion.Schedule;
using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.Events;
using NetAdmin.Host.Filters;
using NetAdmin.SysComponent.Host.Jobs;
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
    public static IServiceCollection AddContextUserInfo(this IServiceCollection me)
    {
        return me.AddScoped(typeof(ContextUserInfo), _ => ContextUserInfo.Create());
    }

    /// <summary>
    ///     添加 freeSql orm工具
    /// </summary>
    public static IServiceCollection AddFreeSql( //
        this IServiceCollection me, FreeSqlInitMethods initMethods = FreeSqlInitMethods.None, Action<IFreeSql> freeSqlConfig = null)
    {
        // // 非调试模式下禁止同步数据库
        // #if !DEBUG
        // initOptions = FreeSqlInitOptions.None;
        // #endif
        var dbOptions      = App.GetOptions<DatabaseOptions>();
        var eventPublisher = App.GetService<IEventPublisher>();

        var fSql = new FreeSqlBuilder(dbOptions).Build(initMethods, count => eventPublisher.PublishAsync(new SeedDataInsertedEvent(count)));
        _ = me.AddSingleton(fSql);

        fSql.Aop.AuditValue += SqlAuditor.DataAuditHandler; // Insert/Update自动值处理

        #pragma warning disable VSTHRD110

        // AOP事件发布（异步）
        fSql.Aop.CommandBefore += (_, e) => eventPublisher.PublishAsync(new SqlCommandBeforeEvent(e)); // 增删查改，执行命令之前触发
        fSql.Aop.CommandAfter  += (_, e) => eventPublisher.PublishAsync(new SqlCommandAfterEvent(e));  // 增删查改，执行命令完成后触发

        fSql.Aop.SyncStructureBefore += (_, e) => eventPublisher.PublishAsync(new SyncStructureBeforeEvent(e)); // CodeFirst迁移，执行之前触发

        fSql.Aop.SyncStructureAfter += (_, e) => eventPublisher.PublishAsync(new SyncStructureAfterEvent(e)); // CodeFirst迁移，执行完成触发
        #pragma warning restore VSTHRD110

        // 全局过滤器设置
        freeSqlConfig?.Invoke(fSql);

        return me.AddScoped<UnitOfWorkManager>()                    // 注入工作单元管理器
                 .AddFreeRepository(null, App.Assemblies.ToArray()) // 批量注入 Repository
                 .AddMvcFilter<TransactionInterceptor>();           // 注入事务拦截器
    }

    /// <summary>
    ///     添加定时任务
    /// </summary>
    public static IServiceCollection AddSchedules(this IServiceCollection me)
    {
        return App.WebHostEnvironment.IsProduction()
            ? me.AddSchedule(      //
                builder => builder //
                           .AddJob<ScheduledJob>(true,     Triggers.PeriodSeconds(1).SetRunOnStart(true))
                           .AddJob<FreeScheduledJob>(true, Triggers.PeriodMinutes(1).SetRunOnStart(true)))
            : me;
    }
}