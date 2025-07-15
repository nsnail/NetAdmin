namespace NetAdmin.Domain.Dto.Sys.Dev;

/// <summary>
///     请求：获取所有数据类型
/// </summary>
public sealed record GetDotnetDataTypesReq : DataAbstraction
{
    /// <summary>
    ///     开始匹配
    /// </summary>
    [Required]
    public string StartWith { get; init; }
}