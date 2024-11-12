namespace NetAdmin.SysComponent.Domain.Dto.Sys;

/// <summary>
///     响应：获取条形图数据
/// </summary>
public sealed record GetBarChartRsp : DataAbstraction
{
    /// <summary>
    ///     时间戳
    /// </summary>
    public DateTime Timestamp { get; init; }

    /// <summary>
    ///     值
    /// </summary>
    public int Value { get; init; }
}