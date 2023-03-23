namespace NetAdmin.Domain.Dto.Sys.Dev;

/// <summary>
///     请求：生成图标代码
/// </summary>
public record GenerateIconCodeReq : DataAbstraction
{
    /// <summary>
    ///     图标名称
    /// </summary>
    [Required]
    public string IconName { get; init; }

    /// <summary>
    ///     前端项目路径
    /// </summary>
    [Required]
    public string ProjectPath { get; init; }

    /// <summary>
    ///     图标代码
    /// </summary>
    [Required]
    public string SvgCode { get; init; }
}