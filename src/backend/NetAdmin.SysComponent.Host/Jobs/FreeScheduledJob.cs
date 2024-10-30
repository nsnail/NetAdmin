using Furion.Schedule;
using NetAdmin.Host.BackgroundRunning;
using NetAdmin.Host.Middlewares;

namespace NetAdmin.SysComponent.Host.Jobs;

/// <summary>
///     释放计划作业
/// </summary>
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
    /// <param name="context">作业执行前上下文</param>
    /// <param name="stoppingToken">取消任务 Token</param>
    /// <exception cref="NetAdminGetLockerException">加锁失败异常</exception>
    public async Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
    {
        if (SafetyShopHostMiddleware.IsShutdown) {
            Console.WriteLine(Ln.此节点已下线);
            return;
        }

        await WorkflowAsync(true, stoppingToken).ConfigureAwait(false);
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