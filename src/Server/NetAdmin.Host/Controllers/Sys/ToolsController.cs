using Microsoft.AspNetCore.Authorization;
using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;

namespace NetAdmin.Host.Controllers.Sys;

/// <summary>
///     工具服务
/// </summary>
public class ToolsController : ControllerBase<IToolsService>, IToolsModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ToolsController" /> class.
    /// </summary>
    public ToolsController(IToolsService service) //
        : base(service) { }

    /// <summary>
    ///     运行环境
    /// </summary>
    public dynamic EnvironmentInfo()
    {
        return Service.EnvironmentInfo();
    }

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