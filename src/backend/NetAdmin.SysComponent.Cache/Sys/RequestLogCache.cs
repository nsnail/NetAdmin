using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.RequestLog;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <summary>
///     请求日志缓存
/// </summary>
public sealed class RequestLogCache : DistributedCache<IRequestLogService>, IScoped, IRequestLogCache
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RequestLogCache" /> class.
    /// </summary>
    public RequestLogCache(IDistributedCache cache, IRequestLogService service) //
        : base(cache, service) { }

    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryRequestLogRsp> CreateAsync(CreateRequestLogReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryRequestLogReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryRequestLogRsp> GetAsync(QueryRequestLogReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryRequestLogRsp>> PagedQueryAsync(PagedQueryReq<QueryRequestLogReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryRequestLogRsp>> QueryAsync(QueryReq<QueryRequestLogReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<NopReq> UpdateAsync(NopReq req)
    {
        return Service.UpdateAsync(req);
    }
}