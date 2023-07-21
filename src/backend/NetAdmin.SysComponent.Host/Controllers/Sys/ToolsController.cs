using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     工具服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class ToolsController : ControllerBase<IToolsService>, IToolsModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ToolsController" /> class.
    /// </summary>
    public ToolsController(IToolsService service) //
        : base(service) { }

    /// <summary>
    ///     服务器时间
    /// </summary>
    [AllowAnonymous]
    public DateTime GetServerUtcTime()
    {
        return Service.GetServerUtcTime();
    }

    /// <summary>
    ///     版本信息
    /// </summary>
    [AllowAnonymous]
    public string Version()
    {
        return Service.Version();
    }
}