namespace NetAdmin.Infrastructure.Schedule;

/// <summary>
///     作业处理程序
/// </summary>
public interface IJob
{
    /// <summary>
    ///     具体处理逻辑
    /// </summary>
    Task ExecuteAsync(CancellationToken cancelToken);
}