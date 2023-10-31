using NetAdmin.Application.Services;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IToolsService" />
public sealed class ToolsService : ServiceBase<IToolsService>, IToolsService
{
    /// <inheritdoc />
    public Task<DateTime> GetServerUtcTimeAsync()
    {
        return Task.FromResult(DateTime.UtcNow);
    }

    /// <inheritdoc />
    public Task<string> VersionAsync()
    {
        return Task.FromResult(GlobalStatic.ProductVersion);
    }
}