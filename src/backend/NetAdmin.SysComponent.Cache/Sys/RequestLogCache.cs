using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.RequestLog;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IRequestLogCache" />
public sealed class RequestLogCache(IDistributedCache cache, IRequestLogService service) //
    : DistributedCache<IRequestLogService>(cache, service), IScoped, IRequestLogCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryRequestLogReq> req)
    {
        return Service.CountAsync(req);
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
    public Task<IActionResult> ExportAsync(QueryReq<QueryRequestLogReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryRequestLogRsp> GetAsync(QueryRequestLogReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<GetBarChartRsp>> GetBarChartAsync(QueryReq<QueryRequestLogReq> req)
    {
        #if !DEBUG
        return GetOrCreateAsync(                                                  //
            GetCacheKey(req.GetHashCode().ToString(CultureInfo.InvariantCulture)) //
          , () => Service.GetBarChartAsync(req), TimeSpan.FromSeconds(Numbers.SECS_CACHE_CHART));
        #else
        return Service.GetBarChartAsync(req);
        #endif
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<GetPieChartRsp>> GetPieChartByApiSummaryAsync(QueryReq<QueryRequestLogReq> req)
    {
        #if !DEBUG
        return GetOrCreateAsync(                                                  //
            GetCacheKey(req.GetHashCode().ToString(CultureInfo.InvariantCulture)) //
          , () => Service.GetPieChartByApiSummaryAsync(req), TimeSpan.FromSeconds(Numbers.SECS_CACHE_CHART));
        #else
        return Service.GetPieChartByApiSummaryAsync(req);
        #endif
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<GetPieChartRsp>> GetPieChartByHttpStatusCodeAsync(QueryReq<QueryRequestLogReq> req)
    {
        #if !DEBUG
        return GetOrCreateAsync(                                                  //
            GetCacheKey(req.GetHashCode().ToString(CultureInfo.InvariantCulture)) //
          , () => Service.GetPieChartByHttpStatusCodeAsync(req), TimeSpan.FromSeconds(Numbers.SECS_CACHE_CHART));
        #else
        return Service.GetPieChartByHttpStatusCodeAsync(req);
        #endif
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
}