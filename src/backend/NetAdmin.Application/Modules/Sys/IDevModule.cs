using NetAdmin.DataContract.Dto.Sys.Dev;

namespace NetAdmin.Application.Modules.Sys;

/// <summary>
///     开发模块
/// </summary>
public interface IDevModule
{
    /// <summary>
    ///     生成后端代码
    /// </summary>
    ValueTask GenerateCsCode(GenerateCsCodeReq req);

    /// <summary>
    ///     生成图标代码
    /// </summary>
    ValueTask GenerateIconCode(GenerateIconCodeReq req);

    /// <summary>
    ///     生成接口代码
    /// </summary>
    /// <param name="projectPath">前端项目目录路径</param>
    ValueTask GenerateJsCode(string projectPath);
}