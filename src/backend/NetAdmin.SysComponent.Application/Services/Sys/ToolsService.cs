using NetAdmin.Application.Services;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IToolsService" />
public sealed class ToolsService : ServiceBase<IToolsService>, IToolsService
{
    /// <summary>
    ///     服务器时间
    /// </summary>
    public DateTime GetServerUtcTime()
    {
        return DateTime.UtcNow;
    }

    /// <summary>
    ///     版本信息
    /// </summary>
    public string Version()
    {
        return Global.ProductVersion;
    }
}