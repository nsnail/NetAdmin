using NetAdmin.Host.BackgroundRunning;
using NetAdmin.Infrastructure.Schedule;

namespace NetAdmin.SysComponent.Host.Jobs;

/// <summary>
///     释放计划作业
/// </summary>
[JobConfig(TriggerCron = "0 * * * * *")]
public sealed class FreeScheduledJob : WorkBase<FreeScheduledJob>, IJob
{
    private readonly IJobService _jobService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="FreeScheduledJob" /> class.
    /// </summary>
    public FreeScheduledJob()
    {
        _jobService = ServiceProvider.GetService<IJobService>();
    }

    /// <summary>
    ///     具体处理逻辑
    /// </summary>
    /// <param name="cancelToken">取消任务 Token</param>
    /// <exception cref="NetAdminGetLockerException">加锁失败异常</exception>
    public async Task ExecuteAsync(CancellationToken cancelToken)
    {
        await WorkflowAsync(true, cancelToken).ConfigureAwait(false);
    }

    /// <summary>
    ///     通用工作流
    /// </summary>
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    /// <exception cref="ArgumentOutOfRangeException">ArgumentOutOfRangeException</exception>
    protected override async ValueTask WorkflowAsync(CancellationToken cancelToken)
    {
        _ = await _jobService.ReleaseStuckTaskAsync().ConfigureAwait(false);
    }
}