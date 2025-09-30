using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.RequestLog;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IRequestLogCache" />
public sealed class RequestLogCache(IDistributedCache cache, IRequestLogService service)
    : DistributedCache<IRequestLogService>(cache, service), IScoped, IRequestLogCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req) {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    #if !DEBUG
    public async Task<long> CountAsync(QueryReq<QueryRequestLogReq> req)
    #else
    public Task<long> CountAsync(QueryReq<QueryRequestLogReq> req)
        #endif
    {
        #if !DEBUG
        var ret = await GetOrCreateAsync(
                GetCacheKey(req.Json().Crc32().ToString(CultureInfo.InvariantCulture))
                , async () => (long?)await Service.CountAsync(req).ConfigureAwait(false), TimeSpan.FromSeconds(Numbers.SECS_CACHE_DEFAULT)
            )
            .ConfigureAwait(false);
        return ret ?? 0;
        #else
        return Service.CountAsync(req);
        #endif
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryRequestLogReq> req) {
        return Service.CountByAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryRequestLogRsp> CreateAsync(CreateRequestLogReq req) {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req) {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryRequestLogRsp> EditAsync(EditRequestLogReq req) {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryRequestLogReq> req) {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryRequestLogRsp> GetAsync(QueryRequestLogReq req) {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<GetBarChartRsp>> GetBarChartAsync(QueryReq<QueryRequestLogReq> req) {
        #if !DEBUG
        return GetOrCreateAsync(
            GetCacheKey(req.Json().Crc32().ToString(CultureInfo.InvariantCulture)), () => Service.GetBarChartAsync(req)
            , TimeSpan.FromSeconds(Numbers.SECS_CACHE_CHART)
        );
        #else
        return Service.GetBarChartAsync(req);
        #endif
    }

    /// <inheritdoc />
    public Task<IEnumerable<GetPieChartRsp>> GetPieChartByApiSummaryAsync(QueryReq<QueryRequestLogReq> req) {
        #if !DEBUG
        return GetOrCreateAsync(
            GetCacheKey(req.Json().Crc32().ToString(CultureInfo.InvariantCulture)), () => Service.GetPieChartByApiSummaryAsync(req)
            , TimeSpan.FromSeconds(Numbers.SECS_CACHE_CHART)
        );
        #else
        return Service.GetPieChartByApiSummaryAsync(req);
        #endif
    }

    /// <inheritdoc />
    public Task<IEnumerable<GetPieChartRsp>> GetPieChartByHttpStatusCodeAsync(QueryReq<QueryRequestLogReq> req) {
        #if !DEBUG
        return GetOrCreateAsync(
            GetCacheKey(req.Json().Crc32().ToString(CultureInfo.InvariantCulture)), () => Service.GetPieChartByHttpStatusCodeAsync(req)
            , TimeSpan.FromSeconds(Numbers.SECS_CACHE_CHART)
        );
        #else
        return Service.GetPieChartByHttpStatusCodeAsync(req);
        #endif
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryRequestLogRsp>> PagedQueryAsync(PagedQueryReq<QueryRequestLogReq> req) {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryRequestLogRsp>> QueryAsync(QueryReq<QueryRequestLogReq> req) {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<decimal> SumAsync(QueryReq<QueryRequestLogReq> req) {
        return Service.SumAsync(req);
    }
}