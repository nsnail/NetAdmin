using NetAdmin.Application.Services;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IToolsService" />
public sealed class ToolsService : ServiceBase<IToolsService>, IToolsService
{
    /// <inheritdoc />
    public DateTime GetServerUtcTime()
    {
        return DateTime.UtcNow;
    }

    /// <inheritdoc />
    public string Version()
    {
        return GlobalStatic.ProductVersion;
    }
}