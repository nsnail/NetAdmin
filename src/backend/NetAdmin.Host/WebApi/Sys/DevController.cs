using System.ComponentModel.DataAnnotations;
using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.DataContract.Dto.Sys.Dev;

namespace NetAdmin.Host.WebApi.Sys;

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
    public async Task GenerateCsCode(GenerateCsCodeReq req)
    {
        await Service.GenerateCsCode(req);
    }

    /// <summary>
    ///     生成图标代码
    /// </summary>
    public async Task GenerateIconCode(GenerateIconCodeReq req)
    {
        await Service.GenerateIconCode(req);
    }

    /// <summary>
    ///     生成接口代码
    /// </summary>
    /// <param name="projectPath">前端项目Src目录路径</param>
    public async Task GenerateJsCode([Required] string projectPath)
    {
        await Service.GenerateJsCode(projectPath);
    }
}