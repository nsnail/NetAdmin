namespace NetAdmin.Domain.Dto.Sys.Dev;

/// <summary>
///     请求：生成后端代码
/// </summary>
public sealed record GenerateCsCodeReq : DataAbstraction
{
    /// <summary>
    ///     基类
    /// </summary>
    public string BaseClass { get; init; }

    /// <summary>
    ///     实体名称
    /// </summary>
    public string EntityName { get; init; }

    /// <summary>
    ///     字段列表
    /// </summary>
    public IReadOnlyCollection<FieldItemInfo> FieldList { get; init; }

    /// <summary>
    ///     接口列表
    /// </summary>
    public string[] Interfaces { get; init; }

    /// <summary>
    ///     项目
    /// </summary>
    public string Project { get; init; }

    /// <summary>
    ///     描述
    /// </summary>
    public string Summary { get; set; }
}