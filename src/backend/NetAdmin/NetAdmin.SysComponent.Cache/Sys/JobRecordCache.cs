using NetAdmin.SysComponent.Domain.Dto.Sys.JobRecord;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IJobRecordCache" />
public sealed class JobRecordCache(IDistributedCache cache, IJobRecordService service)
    : DistributedCache<IJobRecordService>(cache, service), IScoped, IJobRecordCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryJobRecordReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryJobRecordRsp> CreateAsync(CreateJobRecordReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryJobRecordReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryJobRecordReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryJobRecordRsp> GetAsync(QueryJobRecordReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryJobRecordRsp>> PagedQueryAsync(PagedQueryReq<QueryJobRecordReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryJobRecordRsp>> QueryAsync(QueryReq<QueryJobRecordReq> req)
    {
        return Service.QueryAsync(req);
    }
}