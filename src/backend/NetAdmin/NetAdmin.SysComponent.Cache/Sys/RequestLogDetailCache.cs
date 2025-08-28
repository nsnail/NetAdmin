using NetAdmin.Domain.Dto.Sys.RequestLogDetail;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IRequestLogDetailCache" />
public sealed class RequestLogDetailCache(IDistributedCache cache, IRequestLogDetailService service)
    : DistributedCache<IRequestLogDetailService>(cache, service), IScoped, IRequestLogDetailCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req) {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryRequestLogDetailReq> req) {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryRequestLogDetailReq> req) {
        return Service.CountByAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryRequestLogDetailRsp> CreateAsync(CreateRequestLogDetailReq req) {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req) {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryRequestLogDetailRsp> EditAsync(EditRequestLogDetailReq req) {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryRequestLogDetailReq> req) {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryRequestLogDetailRsp> GetAsync(QueryRequestLogDetailReq req) {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryRequestLogDetailRsp>> PagedQueryAsync(PagedQueryReq<QueryRequestLogDetailReq> req) {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryRequestLogDetailRsp>> QueryAsync(QueryReq<QueryRequestLogDetailReq> req) {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<decimal> SumAsync(QueryReq<QueryRequestLogDetailReq> req) {
        return Service.SumAsync(req);
    }
}