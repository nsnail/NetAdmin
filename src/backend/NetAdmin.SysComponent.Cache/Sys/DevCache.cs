using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Sys.Dev;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IDevCache" />
public sealed class DevCache : DistributedCache<IDevService>, IScoped, IDevCache
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DevCache" /> class.
    /// </summary>
    public DevCache(IDistributedCache cache, IDevService service) //
        : base(cache, service) { }

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