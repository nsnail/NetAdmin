using NetAdmin.Domain.Dto.Sys.Dev;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     开发模块
/// </summary>
public interface IDevModule
{
    /// <summary>
    ///     生成后端代码
    /// </summary>
    Task GenerateCsCodeAsync(GenerateCsCodeReq req);

    /// <summary>
    ///     生成图标代码
    /// </summary>
    Task GenerateIconCodeAsync(GenerateIconCodeReq req);

    /// <summary>
    ///     生成接口代码
    /// </summary>
    Task GenerateJsCodeAsync();
}