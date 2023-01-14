using NetAdmin.DataContract.Dto.Sys.Dev;

namespace NetAdmin.Application.Services.Sys;

/// <summary>
///     开发服务
/// </summary>
public interface IDevService : IService
{
    /// <summary>
    ///     生成图标代码
    /// </summary>
    ValueTask GenerateIconCode(GenerateIconCodeReq req);

    /// <summary>
    ///     生成接口代码
    /// </summary>
    /// <param name="projectPath">前端项目Src目录路径</param>
    ValueTask GenerateJsCode(string projectPath);
}