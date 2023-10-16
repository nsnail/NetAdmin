using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IToolsCache" />
public sealed class ToolsCache
    (IDistributedCache cache, IToolsService service) : DistributedCache<IToolsService>(cache, service), IScoped
                                                     , IToolsCache
{
    /// <inheritdoc />
    public DateTime GetServerUtcTime()
    {
        return Service.GetServerUtcTime();
    }

    /// <inheritdoc />
    public string Version()
    {
        return Service.Version();
    }
}