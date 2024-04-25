namespace NetAdmin.Domain.Dto.Sys;

/// <summary>
///     响应：获取饼图数据
/// </summary>
public sealed record GetPieChartRsp : DataAbstraction
{
    /// <summary>
    ///     键名
    /// </summary>
    public string Key { get; init; }

    /// <summary>
    ///     键值
    /// </summary>
    public int Value { get; init; }
}