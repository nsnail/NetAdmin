using NetAdmin.SysComponent.Domain.Dto.Sys.SiteMsgDept;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="ISiteMsgDeptCache" />
public sealed class SiteMsgDeptCache(IDistributedCache cache, ISiteMsgDeptService service)
    : DistributedCache<ISiteMsgDeptService>(cache, service), IScoped, ISiteMsgDeptCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QuerySiteMsgDeptReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgDeptRsp> CreateAsync(CreateSiteMsgDeptReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QuerySiteMsgDeptReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QuerySiteMsgDeptReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgDeptRsp> GetAsync(QuerySiteMsgDeptReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QuerySiteMsgDeptRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgDeptReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QuerySiteMsgDeptRsp>> QueryAsync(QueryReq<QuerySiteMsgDeptReq> req)
    {
        return Service.QueryAsync(req);
    }
}