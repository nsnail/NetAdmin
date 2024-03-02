using NetAdmin.Domain.Dto.Sys.Tool;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     工具服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class ToolsController(IToolsCache cache) : ControllerBase<IToolsCache, IToolsService>(cache), IToolsModule
{
    /// <summary>
    ///     获取更新日志
    /// </summary>
    [AllowAnonymous]
    public Task<string> GetChangeLogAsync()
    {
        return Cache.GetChangeLogAsync();
    }

    /// <summary>
    ///     获取模块信息
    /// </summary>
    [AllowAnonymous]
    public Task<IEnumerable<GetModulesRsp>> GetModulesAsync()
    {
        return Cache.GetModulesAsync();
    }

    /// <summary>
    ///     获取服务器时间
    /// </summary>
    [AllowAnonymous]
    public Task<DateTime> GetServerUtcTimeAsync()
    {
        return Cache.GetServerUtcTimeAsync();
    }

    /// <summary>
    ///     获取版本信息
    /// </summary>
    [AllowAnonymous]
    public Task<string> GetVersionAsync()
    {
        return Cache.GetVersionAsync();
    }
}