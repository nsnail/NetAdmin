using Furion.Schedule;
using NetAdmin.Host.BackgroundRunning;

namespace NetAdmin.ScheduledService.Jobs;

/// <summary>
///     示例Job
/// </summary>
public sealed class ExampleJob : WorkBase<ExampleJob>, IJob
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ExampleJob" /> class.
    /// </summary>
    public ExampleJob()
    {
    }

    /// <inheritdoc />
    public async Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
    {
        await WorkflowAsync(stoppingToken);
    }

    /// <inheritdoc />
    protected override ValueTask WorkflowAsync(CancellationToken cancelToken)
    {
        return ValueTask.CompletedTask;
    }
}