using NetAdmin.Domain.Dto.Sys.Dept;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IDeptCache" />
public sealed class DeptCache(IDistributedCache cache, IDeptService service) //
    : DistributedCache<IDeptService>(cache, service), IScoped, IDeptCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryDeptReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDeptRsp> CreateAsync(CreateDeptReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDeptRsp> EditAsync(EditDeptReq req)
    {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryDeptReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryDeptReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDeptRsp> GetAsync(QueryDeptReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDeptRsp>> PagedQueryAsync(PagedQueryReq<QueryDeptReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryDeptRsp>> QueryAsync(QueryReq<QueryDeptReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<int> SetEnabledAsync(SetDeptEnabledReq req)
    {
        return Service.SetEnabledAsync(req);
    }
}