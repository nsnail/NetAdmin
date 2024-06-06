using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.Job;
using NetAdmin.Domain.Dto.Sys.JobRecord;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IJobCache" />
public sealed class JobCache(IDistributedCache cache, IJobService service)
    : DistributedCache<IJobService>(cache, service), IScoped, IJobCache
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
    public Task<bool> ExistAsync(QueryReq<QueryJobReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryJobRsp> GetAsync(QueryJobReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<GetBarChartRsp>> GetRecordBarChartAsync(QueryReq<QueryJobRecordReq> req)
    {
        #if !DEBUG
        return GetOrCreateAsync(                                                  //
            GetCacheKey(req.GetHashCode().ToString(CultureInfo.InvariantCulture)) //
          , () => Service.GetRecordBarChartAsync(req), TimeSpan.FromSeconds(Numbers.SECS_CACHE_CHART));
        #else
        return Service.GetRecordBarChartAsync(req);
        #endif
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<GetPieChartRsp>> GetRecordPieChartByHttpStatusCodeAsync(
        QueryReq<QueryJobRecordReq> req)
    {
        #if !DEBUG
        return GetOrCreateAsync(                                                  //
            GetCacheKey(req.GetHashCode().ToString(CultureInfo.InvariantCulture)) //
          , () => Service.GetRecordPieChartByHttpStatusCodeAsync(req)
          , TimeSpan.FromSeconds(Numbers.SECS_CACHE_DEFAULT));
        #else
        return Service.GetRecordPieChartByHttpStatusCodeAsync(req);
        #endif
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<GetPieChartRsp>> GetRecordPieChartByNameAsync(QueryReq<QueryJobRecordReq> req)
    {
        #if !DEBUG
        return GetOrCreateAsync(                                                  //
            GetCacheKey(req.GetHashCode().ToString(CultureInfo.InvariantCulture)) //
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
    public Task<IEnumerable<QueryJobRsp>> QueryAsync(QueryReq<QueryJobReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryJobRecordRsp> RecordGetAsync(QueryJobRecordReq req)
    {
        return Service.RecordGetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryJobRecordRsp>> RecordPagedQueryAsync(PagedQueryReq<QueryJobRecordReq> req)
    {
        return Service.RecordPagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task SetEnabledAsync(SetJobEnabledReq req)
    {
        return Service.SetEnabledAsync(req);
    }
}