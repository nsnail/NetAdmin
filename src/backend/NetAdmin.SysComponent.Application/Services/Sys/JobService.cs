using Cronos;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.Job;
using NetAdmin.Domain.Dto.Sys.JobRecord;
using NetAdmin.Domain.Enums.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using DataType = FreeSql.DataType;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IJobService" />
public sealed class JobService(DefaultRepository<Sys_Job> rpo, IJobRecordService jobRecordService) //
    : RepositoryService<Sys_Job, IJobService>(rpo), IJobService
{
    /// <inheritdoc />
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        req.ThrowIfInvalid();
        var ret = 0;

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var item in req.Items) {
            ret += await DeleteAsync(item).ConfigureAwait(false);
        }

        return ret;
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryJobReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).CountAsync();
    }

    /// <inheritdoc />
    public async Task<QueryJobRsp> CreateAsync(CreateJobReq req)
    {
        req.ThrowIfInvalid();
        var nextExecTime = GetNextExecTime(req.ExecutionCron);
        var ret = await Rpo.InsertAsync(req with {
                                                     NextExecTime = nextExecTime
                                                   , NextTimeId = nextExecTime?.TimeUnixUtc()
                                                   , RequestHeader = req.RequestHeaders?.Json()
                                                 })
                           .ConfigureAwait(false);
        return ret.Adapt<QueryJobRsp>();
    }

    /// <inheritdoc />
    public async Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.DeleteCascadeByDatabaseAsync(a => a.Id == req.Id).ConfigureAwait(false);
        return ret.Count;
    }

    /// <inheritdoc />
    public async Task<QueryJobRsp> EditAsync(UpdateJobReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.UpdateDiy.Set(a => a.ExecutionCron == req.ExecutionCron)
                           .Set(a => a.HttpMethod              == req.HttpMethod)
                           .Set(a => a.JobName                 == req.JobName)
                           .SetIf(req.RequestHeaders == null, a => a.RequestHeader, null)
                           .SetIf(req.RequestHeaders != null, a => a.RequestHeader, req.RequestHeaders.Json())
                           .Set(a => a.RequestBody == req.RequestBody)
                           .Set(a => a.RequestUrl  == req.RequestUrl)
                           .Set(a => a.UserId      == req.UserId)
                           .Where(a => a.Id        == req.Id)
                           .ExecuteUpdatedAsync()
                           .ConfigureAwait(false);
        return ret[0].Adapt<QueryJobRsp>();
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryJobReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).AnyAsync();
    }

    /// <inheritdoc />
    public async Task FinishJobAsync(UpdateJobReq req)
    {
        req.ThrowIfInvalid();
        var nextExecTime = GetNextExecTime(req.ExecutionCron);
        _ = await UpdateAsync(req with {
                                           Status = JobStatues.Idle
                                         , NextExecTime = nextExecTime
                                         , NextTimeId = nextExecTime?.TimeUnixUtc()
                                       })
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<QueryJobRsp> GetAsync(QueryJobReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryJobReq> { Filter = req }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryJobRsp>();
    }

    /// <inheritdoc />
    public async Task<QueryJobRsp> GetNextJobAsync()
    {
        var df = new DynamicFilterInfo {
                                           Filters = [
                                               new DynamicFilterInfo {
                                                                         Field    = nameof(QueryJobReq.NextExecTime)
                                                                       , Value    = DateTime.UtcNow
                                                                       , Operator = DynamicFilterOperators.LessThan
                                                                     }
                                             , new DynamicFilterInfo {
                                                                         Field    = nameof(QueryJobReq.Status)
                                                                       , Value    = JobStatues.Idle
                                                                       , Operator = DynamicFilterOperators.Eq
                                                                     }
                                             , new DynamicFilterInfo {
                                                                         Field    = nameof(QueryJobReq.Enabled)
                                                                       , Value    = true
                                                                       , Operator = DynamicFilterOperators.Eq
                                                                     }
                                           ]
                                       };
        var job
            = await QueryInternal(new QueryReq<QueryJobReq> { DynamicFilter = df, Count = 1, Order = Orders.Random })
                    .Where(a => !Rpo.Orm.Select<Sys_JobRecord>()
                                    .As("b")
                                    .Where(b => b.JobId == a.Id && b.TimeId == a.NextTimeId)
                                    .Any())
                    .ToOneAsync()
                    .ConfigureAwait(false);
        return job == null
            ? null
            : await UpdateAsync(job.Adapt<UpdateJobReq>() with {
                                                                   Status = JobStatues.Running
                                                                 , LastExecTime = DateTime.UtcNow
                                                               })
                .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<GetBarChartRsp>> GetRecordBarChartAsync(QueryReq<QueryJobRecordReq> req)
    {
        req.ThrowIfInvalid();
        return jobRecordService.GetBarChartAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<GetPieChartRsp>> GetRecordPieChartByHttpStatusCodeAsync(
        QueryReq<QueryJobRecordReq> req)
    {
        req.ThrowIfInvalid();
        return jobRecordService.GetPieChartByHttpStatusCodeAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<GetPieChartRsp>> GetRecordPieChartByNameAsync(QueryReq<QueryJobRecordReq> req)
    {
        req.ThrowIfInvalid();
        return jobRecordService.GetPieChartByNameAsync(req);
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryJobRsp>> PagedQueryAsync(PagedQueryReq<QueryJobReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .Count(out var total)
                         .ToListAsync()
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryJobRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryJobRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryJobRsp>> QueryAsync(QueryReq<QueryJobReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryJobRsp>>();
    }

    /// <inheritdoc />
    public Task<QueryJobRecordRsp> RecordGetAsync(QueryJobRecordReq req)
    {
        req.ThrowIfInvalid();
        return jobRecordService.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryJobRecordRsp>> RecordPagedQueryAsync(PagedQueryReq<QueryJobRecordReq> req)
    {
        req.ThrowIfInvalid();
        return jobRecordService.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<int> ReleaseStuckTaskAsync()
    {
        return Rpo.UpdateDiy.Set(a => a.Status == JobStatues.Idle)
                  .Where(a => a.Status       == JobStatues.Running &&
                              a.LastExecTime < DateTime.Now.AddSeconds(-Numbers.SECS_TIMEOUT_JOB))
                  .ExecuteAffrowsAsync();
    }

    /// <inheritdoc />
    public Task SetEnabledAsync(UpdateJobReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.UpdateDiy.Set(a => a.Enabled == req.Enabled).Where(a => a.Id == req.Id).ExecuteAffrowsAsync();
    }

    /// <inheritdoc />
    public async Task<QueryJobRsp> UpdateAsync(UpdateJobReq req)
    {
        req.ThrowIfInvalid();
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return (await UpdateForSqliteAsync(req).ConfigureAwait(false)).Adapt<QueryJobRsp>();
        }

        _ = await Rpo.UpdateAsync(req).ConfigureAwait(false);
        return req.Adapt<QueryJobRsp>();
    }

    /// <inheritdoc />
    protected override async Task<Sys_Job> UpdateForSqliteAsync(Sys_Job req)
    {
        _ = await Rpo.UpdateAsync(req).ConfigureAwait(false);
        return req;
    }

    private static DateTime? GetNextExecTime(string cron)
    {
        return CronExpression.Parse(cron, CronFormat.IncludeSeconds)
                             .GetNextOccurrence(DateTime.UtcNow, TimeZoneInfo.Utc);
    }

    private ISelect<Sys_Job> QueryInternal(QueryReq<QueryJobReq> req)
    {
        var ret = Rpo.Select.Include(a => a.User)
                     .WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .WhereIf( //
                         req.Keywords?.Length > 0
                       , a => a.Id == req.Keywords.Int64Try(0) || a.JobName.Contains(req.Keywords));
        switch (req.Order) {
            case Orders.None:
                return ret;
            case Orders.Random:
                return ret.OrderByRandom();
        }

        ret = ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);
        if (!req.Prop?.Equals(nameof(req.Filter.LastExecTime), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.LastExecTime);
        }

        return ret;
    }
}