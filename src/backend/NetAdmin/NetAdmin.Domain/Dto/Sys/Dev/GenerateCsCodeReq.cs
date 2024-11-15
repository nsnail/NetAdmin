namespace NetAdmin.Domain.Dto.Sys.Dev;

/// <summary>
///     请求：生成后端代码
/// </summary>
public sealed record GenerateCsCodeReq : DataAbstraction
{
    /// <summary>
    ///     模块名称
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.模块名称不能为空))]
    public string ModuleName { get; init; }

    /// <summary>
    ///     模块说明
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.模块说明不能为空))]
    public string ModuleRemark { get; init; }

    /// <summary>
    ///     模块类型
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.模块类型不能为空))]
    public string Type { get; init; }
}