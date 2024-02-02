using Furion.Schedule;
using NetAdmin.SysComponent.Host.Jobs;

namespace NetAdmin.SysComponent.Host.Extensions;

/// <summary>
///     ServiceCollection 扩展方法
/// </summary>
[SuppressSniffer]
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     添加定时任务
    /// </summary>
    public static IServiceCollection AddSchedules(this IServiceCollection me)
    {
        return me.AddSchedule( //
            builder => builder //
                .AddJob<ScheduledJob>(false, Triggers.PeriodSeconds(5).SetRunOnStart(true)));
    }
}