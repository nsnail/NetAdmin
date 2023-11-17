namespace NetAdmin.Host.BackgroundRunning;

/// <summary>
///     轮询工作接口
/// </summary>
public interface IPollingWork
{
    /// <summary>
    ///     启动工作
    /// </summary>
    ValueTask StartAsync(CancellationToken cancelToken);
}