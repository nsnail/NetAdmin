using NetAdmin.Domain.Dto.Sys.LoginLog;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="ILoginLogCache" />
public sealed class LoginLogCache(IDistributedCache cache, ILoginLogService service)
    : DistributedCache<ILoginLogService>(cache, service), IScoped, ILoginLogCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryLoginLogReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryLoginLogRsp> CreateAsync(CreateLoginLogReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryLoginLogRsp> EditAsync(EditLoginLogReq req)
    {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryLoginLogReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryLoginLogRsp> GetAsync(QueryLoginLogReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryLoginLogRsp>> PagedQueryAsync(PagedQueryReq<QueryLoginLogReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryLoginLogRsp>> QueryAsync(QueryReq<QueryLoginLogReq> req)
    {
        return Service.QueryAsync(req);
    }
}