using NetAdmin.Domain.Dto.Sys.Dev;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     开发服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class DevController : ControllerBase<IDevCache, IDevService>, IDevModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DevController" /> class.
    /// </summary>
    public DevController(IDevCache cache) //
        : base(cache) { }

    /// <summary>
    ///     生成后端代码
    /// </summary>
    public Task GenerateCsCodeAsync(GenerateCsCodeReq req)
    {
        return Cache.GenerateCsCodeAsync(req);
    }

    /// <summary>
    ///     生成图标代码
    /// </summary>
    public Task GenerateIconCodeAsync(GenerateIconCodeReq req)
    {
        return Cache.GenerateIconCodeAsync(req);
    }

    /// <summary>
    ///     生成接口代码
    /// </summary>
    public Task GenerateJsCodeAsync()
    {
        return Cache.GenerateJsCodeAsync();
    }
}