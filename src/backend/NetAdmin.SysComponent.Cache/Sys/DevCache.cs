using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Sys.Dev;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IDevCache" />
public sealed class DevCache
    (IDistributedCache cache, IDevService service) : DistributedCache<IDevService>(cache, service), IScoped, IDevCache
{
    /// <inheritdoc />
    public Task GenerateCsCodeAsync(GenerateCsCodeReq req)
    {
        return Service.GenerateCsCodeAsync(req);
    }

    /// <inheritdoc />
    public Task GenerateIconCodeAsync(GenerateIconCodeReq req)
    {
        return Service.GenerateIconCodeAsync(req);
    }

    /// <inheritdoc />
    public Task GenerateJsCodeAsync()
    {
        return Service.GenerateJsCodeAsync();
    }
}