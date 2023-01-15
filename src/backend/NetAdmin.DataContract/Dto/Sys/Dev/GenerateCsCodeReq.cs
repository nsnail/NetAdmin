namespace NetAdmin.DataContract.Dto.Sys.Dev;

/// <summary>
///     请求：生成后端代码
/// </summary>
public record GenerateCsCodeReq : DataAbstraction
{
    /// <summary>
    ///     模块名称
    /// </summary>
    public string ModuleName { get; init; }

    /// <summary>
    ///     模块说明
    /// </summary>
    public string ModuleRemark { get; init; }

    /// <summary>
    ///     项目路径
    /// </summary>
    public string ProjectPath { get; init; }

    /// <summary>
    ///     模块类型
    /// </summary>
    public Enums.ModuleTypes Type { get; init; }
}