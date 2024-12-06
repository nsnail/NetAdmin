using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.Job;
using NetAdmin.Domain.Dto.Sys.JobRecord;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IJobCache" />
public sealed class JobCache(IDistributedCache cache, IJobService service) : DistributedCache<IJobService>(cache, service), IScoped, IJobCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryJobReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountRecordAsync(QueryReq<QueryJobRecordReq> req)
    {
        return Service.CountRecordAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryJobRsp> CreateAsync(CreateJobReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryJobRsp> EditAsync(EditJobReq req)
    {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task ExecuteAsync(QueryJobReq req)
    {
        return Service.ExecuteAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryJobReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportRecordAsync(QueryReq<QueryJobRecordReq> req)
    {
        return Service.ExportRecordAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryJobRsp> GetAsync(QueryJobReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryJobRecordRsp> GetRecordAsync(QueryJobRecordReq req)
    {
        return Service.GetRecordAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<GetBarChartRsp>> GetRecordBarChartAsync(QueryReq<QueryJobRecordReq> req)
    {
        #if !DEBUG
        return GetOrCreateAsync(                                                   //
            GetCacheKey(req.Json().Crc32().ToString(CultureInfo.InvariantCulture)) //
          , () => Service.GetRecordBarChartAsync(req), TimeSpan.FromSeconds(Numbers.SECS_CACHE_CHART));
        #else
        return Service.GetRecordBarChartAsync(req);
        #endif
    }

    /// <inheritdoc />
    public Task<IEnumerable<GetPieChartRsp>> GetRecordPieChartByHttpStatusCodeAsync(QueryReq<QueryJobRecordReq> req)
    {
        #if !DEBUG
        return GetOrCreateAsync(                                                   //
            GetCacheKey(req.Json().Crc32().ToString(CultureInfo.InvariantCulture)) //
          , () => Service.GetRecordPieChartByHttpStatusCodeAsync(req), TimeSpan.FromSeconds(Numbers.SECS_CACHE_DEFAULT));
        #else
        return Service.GetRecordPieChartByHttpStatusCodeAsync(req);
        #endif
    }

    /// <inheritdoc />
    public Task<IEnumerable<GetPieChartRsp>> GetRecordPieChartByNameAsync(QueryReq<QueryJobRecordReq> req)
    {
        #if !DEBUG
        return GetOrCreateAsync(                                                   //
            GetCacheKey(req.Json().Crc32().ToString(CultureInfo.InvariantCulture)) //
          , () => Service.GetRecordPieChartByNameAsync(req), TimeSpan.FromSeconds(Numbers.SECS_CACHE_CHART));
        #else
        return Service.GetRecordPieChartByNameAsync(req);
        #endif
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryJobRsp>> PagedQueryAsync(PagedQueryReq<QueryJobReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryJobRecordRsp>> PagedQueryRecordAsync(PagedQueryReq<QueryJobRecordReq> req)
    {
        return Service.PagedQueryRecordAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryJobRsp>> QueryAsync(QueryReq<QueryJobReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<int> SetEnabledAsync(SetJobEnabledReq req)
    {
        return Service.SetEnabledAsync(req);
    }
}