using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Api;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IApiCache" />
public sealed class ApiCache : DistributedCache<IApiService>, IScoped, IApiCache
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ApiCache" /> class.
    /// </summary>
    public ApiCache(IDistributedCache cache, IApiService service) //
        : base(cache, service) { }

    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryApiRsp> CreateAsync(CreateApiReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryApiReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryApiRsp> GetAsync(QueryApiReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryApiRsp>> PagedQueryAsync(PagedQueryReq<QueryApiReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryApiRsp>> QueryAsync(QueryReq<QueryApiReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task SyncAsync()
    {
        return Service.SyncAsync();
    }

    /// <inheritdoc />
    public Task<NopReq> UpdateAsync(NopReq req)
    {
        return Service.UpdateAsync(req);
    }
}