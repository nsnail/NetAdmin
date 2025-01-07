namespace NetAdmin.Infrastructure.Schedule;

/// <summary>
///     作业配置
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class JobConfigAttribute : Attribute
{
    /// <summary>
    ///     上一次执行时间
    /// </summary>
    public DateTime? LastExecutionTime { get; set; }

    /// <summary>
    ///     启动时运行
    /// </summary>
    public bool RunOnStart { get; init; }

    /// <summary>
    ///     触发器表达式
    /// </summary>
    public string TriggerCron { get; init; }
}