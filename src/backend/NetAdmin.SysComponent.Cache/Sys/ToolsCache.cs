using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <summary>
///     工具缓存
/// </summary>
public sealed class ToolsCache : DistributedCache<IToolsService>, IScoped, IToolsCache
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ToolsCache" /> class.
    /// </summary>
    public ToolsCache(IDistributedCache cache, IToolsService service) //
        : base(cache, service) { }

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