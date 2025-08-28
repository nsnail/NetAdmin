using NetAdmin.Domain.Dto.Sys.Tool;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IToolsCache" />
public sealed class ToolsCache(IDistributedCache cache, IToolsService service) : DistributedCache<IToolsService>(cache, service), IScoped, IToolsCache
{
    /// <inheritdoc />
    public string AesDecode(AesDecodeReq req) {
        return Service.AesDecode(req);
    }

    /// <inheritdoc />
    public Task<object[][]> ExecuteSqlAsync(ExecuteSqlReq req) {
        return Service.ExecuteSqlAsync(req);
    }

    /// <inheritdoc />
    public Task<string> GetChangeLogAsync() {
        return Service.GetChangeLogAsync();
    }

    /// <inheritdoc />
    public Task<IEnumerable<GetModulesRsp>> GetModulesAsync() {
        return Service.GetModulesAsync();
    }

    /// <inheritdoc />
    public Task<DateTime> GetServerUtcTimeAsync() {
        return Service.GetServerUtcTimeAsync();
    }

    /// <inheritdoc />
    public Task<string> GetVersionAsync() {
        return Service.GetVersionAsync();
    }
}