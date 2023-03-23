using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Sys.Dev;

namespace NetAdmin.Host.Controllers.Sys;

/// <summary>
///     开发服务
/// </summary>
public class DevController : ControllerBase<IDevService>, IDevModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DevController" /> class.
    /// </summary>
    public DevController(IDevService service) //
        : base(service) { }

    /// <summary>
    ///     生成后端代码
    /// </summary>
    public Task GenerateCsCodeAsync(GenerateCsCodeReq req)
    {
        return Service.GenerateCsCodeAsync(req);
    }

    /// <summary>
    ///     生成图标代码
    /// </summary>
    public Task GenerateIconCodeAsync(GenerateIconCodeReq req)
    {
        return Service.GenerateIconCodeAsync(req);
    }

    /// <summary>
    ///     生成接口代码
    /// </summary>
    /// <param name="projectPath">前端项目Src目录路径</param>
    public Task GenerateJsCodeAsync([Required] string projectPath)
    {
        return Service.GenerateJsCodeAsync(projectPath);
    }
}