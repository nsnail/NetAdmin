namespace NetAdmin.SysComponent.Domain.Dto.Sys.Dev;

/// <summary>
///     请求：生成图标代码
/// </summary>
public sealed record GenerateIconCodeReq : DataAbstraction
{
    /// <summary>
    ///     图标名称
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.图标名称不能为空))]
    public string IconName { get; init; }

    /// <summary>
    ///     图标代码
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.图标代码不能为空))]
    public string SvgCode { get; init; }
}