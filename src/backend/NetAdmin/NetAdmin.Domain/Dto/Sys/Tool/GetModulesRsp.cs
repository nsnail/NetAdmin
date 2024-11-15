namespace NetAdmin.Domain.Dto.Sys.Tool;

/// <summary>
///     响应：获取模块信息
/// </summary>
public sealed record GetModulesRsp : DataAbstraction
{
    /// <summary>
    ///     模块名称
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    ///     模块版本
    /// </summary>
    public string Version { get; init; }
}