using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Tpl.Example;
using NetAdmin.SysComponent.Application.Services.Tpl.Dependency;
using NetAdmin.SysComponent.Cache.Tpl.Dependency;

namespace NetAdmin.SysComponent.Cache.Tpl;

/// <inheritdoc cref="IExampleCache" />
public sealed class ExampleCache(IDistributedCache cache, IExampleService service)
    : DistributedCache<IExampleService>(cache, service), IScoped, IExampleCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryExampleRsp> CreateAsync(CreateExampleReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryExampleReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryExampleRsp> GetAsync(QueryExampleReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryExampleRsp>> PagedQueryAsync(PagedQueryReq<QueryExampleReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryExampleRsp>> QueryAsync(QueryReq<QueryExampleReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryExampleRsp> UpdateAsync(UpdateExampleReq req)
    {
        return Service.UpdateAsync(req);
    }
}