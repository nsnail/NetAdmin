using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Sys.Tool;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IToolsCache" />
public sealed class ToolsCache(IDistributedCache cache, IToolsService service) //
    : DistributedCache<IToolsService>(cache, service), IScoped, IToolsCache
{
    /// <inheritdoc />
    public Task<IEnumerable<GetModulesRsp>> GetModulesAsync()
    {
        return Service.GetModulesAsync();
    }

    /// <inheritdoc />
    public Task<DateTime> GetServerUtcTimeAsync()
    {
        return Service.GetServerUtcTimeAsync();
    }

    /// <inheritdoc />
    public Task<string> GetVersionAsync()
    {
        return Service.GetVersionAsync();
    }
}