using Furion.Schedule;
using NetAdmin.ScheduledService.Jobs;

namespace NetAdmin.ScheduledService.Extensions;

/// <summary>
///     ServiceCollection 扩展方法
/// </summary>
[SuppressSniffer]
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     注册定时任务
    /// </summary>
    public static IServiceCollection AddSchedules(this IServiceCollection me)
    {
        _ = me.AddSchedule(    //
            builder => builder //
                .AddJob<ExampleJob>(false, Triggers.Minutely().SetRunOnStart(true))

            // .AddJob<ExampleJob2>(false, Triggers.PeriodSeconds(10).SetRunOnStart(true))
            // .AddJob<ExampleJob3>(false, Triggers.PeriodSeconds(10).SetRunOnStart(true))

            //
            #pragma warning disable SA1009, SA1111
        );
        #pragma warning restore SA1111, SA1009
        return me;
    }
}