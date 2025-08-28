using NetAdmin.Domain.Dto.Sys.Dev;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IDevCache" />
public sealed class DevCache(IDistributedCache cache, IDevService service) : DistributedCache<IDevService>(cache, service), IScoped, IDevCache
{
    /// <inheritdoc />
    public Task GenerateCsCodeAsync(GenerateCsCodeReq req) {
        return Service.GenerateCsCodeAsync(req);
    }

    /// <inheritdoc />
    public Task GenerateIconCodeAsync(GenerateIconCodeReq req) {
        return Service.GenerateIconCodeAsync(req);
    }

    /// <inheritdoc />
    public Task GenerateJsCodeAsync() {
        return Service.GenerateJsCodeAsync();
    }

    /// <inheritdoc />
    public Task<IEnumerable<Tuple<string, string>>> GetDomainProjectsAsync() {
        return Service.GetDomainProjectsAsync();
    }

    /// <inheritdoc />
    public IEnumerable<string> GetDotnetDataTypes(GetDotnetDataTypesReq req) {
        return Service.GetDotnetDataTypes(req);
    }

    /// <inheritdoc />
    public IEnumerable<Tuple<string, string>> GetEntityBaseClasses() {
        return Service.GetEntityBaseClasses();
    }

    /// <inheritdoc />
    public IEnumerable<Tuple<string, string>> GetFieldInterfaces() {
        return Service.GetFieldInterfaces();
    }
}