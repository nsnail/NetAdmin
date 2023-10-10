using NetAdmin.Domain;

namespace NetAdmin.Host.BackgroundRunning;

/// <summary>
///     轮询工作
/// </summary>
public abstract class PollingWork<TWorkData> : WorkBase<TWorkData>
    where TWorkData : DataAbstraction
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PollingWork{TWorkData}" /> class.
    /// </summary>
    protected PollingWork(TWorkData workData)
    {
        WorkData = workData;
    }

    /// <summary>
    ///     工作数据
    /// </summary>
    protected TWorkData WorkData { get; }
}