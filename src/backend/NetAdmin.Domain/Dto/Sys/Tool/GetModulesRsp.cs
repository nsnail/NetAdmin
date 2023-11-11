namespace NetAdmin.Domain.Dto.Sys.Tool;

/// <summary>
///     响应：获取模块信息
/// </summary>
public record GetModulesRsp : DataAbstraction
{
    /// <summary>
    ///     模块名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     模块版本
    /// </summary>
    public string Version { get; set; }
}