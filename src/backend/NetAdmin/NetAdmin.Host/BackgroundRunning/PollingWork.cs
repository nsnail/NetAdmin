using NetAdmin.Domain;

namespace NetAdmin.Host.BackgroundRunning;

/// <summary>
///     轮询工作
/// </summary>
public abstract class PollingWork<TWorkData>(TWorkData workData) : WorkBase<TWorkData>
    where TWorkData : DataAbstraction
{
    /// <summary>
    ///     工作数据
    /// </summary>
    protected TWorkData WorkData => workData;
}