using Cronos;
using FreeSql.Internal;
using NetAdmin.SysComponent.Domain.Dto.Sys.Job;
using NetAdmin.SysComponent.Domain.Dto.Sys.JobRecord;
using NetAdmin.SysComponent.Infrastructure.Constant;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IJobService" />
public sealed class JobService(BasicRepository<Sys_Job, long> rpo, IJobRecordService jobRecordService) //
    : RepositoryService<Sys_Job, long, IJobService>(rpo), IJobService
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
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .CountAsync();
    }

    /// <inheritdoc />
    public Task<long> CountRecordAsync(QueryReq<QueryJobRecordReq> req)
    {
        return jobRecordService.CountAsync(req);
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
    public async Task<QueryJobRsp> EditAsync(EditJobReq req)
    {
        req.ThrowIfInvalid();
        var update = Rpo.UpdateDiy.Set(a => a.ExecutionCron == req.ExecutionCron)
                        .Set(a => a.HttpMethod              == req.HttpMethod)
                        .Set(a => a.JobName                 == req.JobName)
                        .SetIf(req.RequestHeaders == null, a => a.RequestHeader, null)
                        .SetIf(req.RequestHeaders != null, a => a.RequestHeader, req.RequestHeaders.Json())
                        .Set(a => a.RequestBody      == req.RequestBody)
                        .Set(a => a.RequestUrl       == req.RequestUrl)
                        .Set(a => a.RandomDelayBegin == req.RandomDelayBegin)
                        .Set(a => a.RandomDelayEnd   == req.RandomDelayEnd)
                        .Set(a => a.UserId           == req.UserId)
                        .Set(a => a.Summary          == req.Summary)
                        .Where(a => a.Id             == req.Id);

        #if DBTYPE_SQLSERVER
        return (await update.ExecuteUpdatedAsync().ConfigureAwait(false)).FirstOrDefault()?.Adapt<QueryJobRsp>();
        #else
        return await update.ExecuteAffrowsAsync().ConfigureAwait(false) <= 0
            ? null
            : await GetAsync(new QueryJobReq { Id = req.Id }).ConfigureAwait(false);
        #endif
    }

    /// <inheritdoc />
    public async Task ExecuteAsync(QueryJobReq req)
    {
        req.ThrowIfInvalid();
        var df = new DynamicFilterInfo {
                                           Filters = [
                                               new DynamicFilterInfo {
                                                                         Field    = nameof(QueryJobReq.Enabled)
                                                                       , Operator = DynamicFilterOperators.Eq
                                                                       , Value    = true
                                                                     }
                                             , new DynamicFilterInfo {
                                                                         Field    = nameof(QueryJobReq.Status)
                                                                       , Operator = DynamicFilterOperators.Eq
                                                                       , Value    = JobStatues.Idle
                                                                     }
                                           ]
                                       };
        var job = await QueryInternal(new QueryReq<QueryJobReq> { Count = 1, Filter = req, DynamicFilter = df, Order = Orders.None })
                        .ToOneAsync()
                        .ConfigureAwait(false) ?? throw new NetAdminInvalidOperationException(Ln.未获取到待执行任务);

        var nextExecTime = GetNextExecTime(Chars.FLG_CRON_PER_SECS);
        try {
            _ = await UpdateAsync( //
                    job with { NextExecTime = nextExecTime, NextTimeId = nextExecTime?.TimeUnixUtc() }
                  , [nameof(job.NextExecTime), nameof(job.NextTimeId)])
                .ConfigureAwait(false);
        }
        catch (DbUpdateVersionException) {
            throw new NetAdminInvalidOperationException(Ln.并发冲突_请稍后重试);
        }
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryJobReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .AnyAsync();
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryJobReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QueryJobReq, ExportJobRsp>(QueryInternal, req, Ln.计划作业导出);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportRecordAsync(QueryReq<QueryJobRecordReq> req)
    {
        req.ThrowIfInvalid();
        return jobRecordService.ExportAsync(req);
    }

    /// <inheritdoc />
    public async Task FinishJobAsync(FinishJobReq req)
    {
        req.ThrowIfInvalid();
        var nextExecTime = GetNextExecTime(req.ExecutionCron);
        _ = await UpdateAsync( //
                req with { Status = JobStatues.Idle, NextExecTime = nextExecTime, NextTimeId = nextExecTime?.TimeUnixUtc() }
              , [nameof(req.Status), nameof(req.NextExecTime), nameof(req.NextTimeId), nameof(req.LastDuration), nameof(req.LastStatusCode)])
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<QueryJobRsp> GetAsync(QueryJobReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryJobReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryJobRsp>();
    }

    /// <inheritdoc />
    public async Task<QueryJobRsp> GetNextJobAsync()
    {
        var df = new DynamicFilterInfo {
                                           Filters = [
                                               new DynamicFilterInfo {
                                                                         Field    = nameof(QueryJobReq.NextExecTime)
                                                                       , Value    = DateTime.Now
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
        var job = await QueryInternal(new QueryReq<QueryJobReq> { DynamicFilter = df, Order = Orders.Random }, false)
                        #if DBTYPE_SQLSERVER
                        .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                        #endif
                        .Where(a => !Rpo.Orm.Select<Sys_JobRecord>().As("b").Where(b => b.JobId == a.Id && b.TimeId == a.NextTimeId).Any())
                        .ToOneAsync(a => new {
                                                 a.RequestUrl
                                               , a.HttpMethod
                                               , a.RequestHeader
                                               , a.RequestBody
                                               , a.RandomDelayBegin
                                               , a.RandomDelayEnd
                                               , a.UserId
                                               , a.Id
                                               , a.NextTimeId
                                               , a.Version
                                             })
                        .ConfigureAwait(false);
        if (job == null) {
            return null;
        }

        #if DBTYPE_SQLSERVER
        var ret = await UpdateReturnListAsync( //
                job.Adapt<Sys_Job>() with { Status = JobStatues.Running, LastExecTime = DateTime.Now }
              , [nameof(Sys_Job.Status), nameof(Sys_Job.LastExecTime)])
            .ConfigureAwait(false);

        return ret.FirstOrDefault()?.Adapt<QueryJobRsp>();
        #else
        return await UpdateAsync( //
                job.Adapt<Sys_Job>() with { Status = JobStatues.Running, LastExecTime = DateTime.Now }
              , [nameof(Sys_Job.Status), nameof(Sys_Job.LastExecTime)])
            .ConfigureAwait(false) > 0
            ? await GetAsync(new QueryJobReq { Id = job.Id }).ConfigureAwait(false)
            : null;
        #endif
    }

    /// <inheritdoc />
    public Task<QueryJobRecordRsp> GetRecordAsync(QueryJobRecordReq req)
    {
        req.ThrowIfInvalid();
        return jobRecordService.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<GetBarChartRsp>> GetRecordBarChartAsync(QueryReq<QueryJobRecordReq> req)
    {
        req.ThrowIfInvalid();
        return jobRecordService.GetBarChartAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<GetPieChartRsp>> GetRecordPieChartByHttpStatusCodeAsync(QueryReq<QueryJobRecordReq> req)
    {
        req.ThrowIfInvalid();
        return jobRecordService.GetPieChartByHttpStatusCodeAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<GetPieChartRsp>> GetRecordPieChartByNameAsync(QueryReq<QueryJobRecordReq> req)
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
                         #if DBTYPE_SQLSERVER
                         .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                         #endif
                         .Count(out var total)
                         .ToListAsync()
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryJobRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryJobRsp>>());
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryJobRecordRsp>> PagedQueryRecordAsync(PagedQueryReq<QueryJobRecordReq> req)
    {
        req.ThrowIfInvalid();
        return jobRecordService.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryJobRsp>> QueryAsync(QueryReq<QueryJobReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req)
                        #if DBTYPE_SQLSERVER
                        .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                        #endif
                        .Take(req.Count)
                        .ToListAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryJobRsp>>();
    }

    /// <inheritdoc />
    public async Task<int> ReleaseStuckTaskAsync()
    {
        var ret1 = await UpdateAsync( // 运行中，运行时间超过超时设定；置为空闲状态
                new Sys_Job { Status = JobStatues.Idle }, [nameof(Sys_Job.Status)], null
              , a => a.Status == JobStatues.Running && a.LastExecTime < DateTime.Now.AddSeconds(-SysNumbers.SECS_TIMEOUT_JOB), null, true)
            .ConfigureAwait(false);

        var ret2 = await UpdateAsync( // 空闲中，下次执行时间在当前时间减去超时时间以前；将下次执行时间调整到现在
                new Sys_Job { NextExecTime = DateTime.Now, NextTimeId = DateTime.Now.TimeUnixUtc() }
              , [nameof(Sys_Job.NextExecTime), nameof(Sys_Job.NextTimeId)],                                                 null
              , a => a.Status == JobStatues.Idle && a.NextExecTime < DateTime.Now.AddSeconds(-SysNumbers.SECS_TIMEOUT_JOB), null, true)
            .ConfigureAwait(false);
        return ret1 + ret2;
    }

    /// <inheritdoc />
    public Task<int> SetEnabledAsync(SetJobEnabledReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req, [nameof(Sys_Job.Enabled)]);
    }

    private static DateTime? GetNextExecTime(string cron)
    {
        return CronExpression.Parse(cron, CronFormat.IncludeSeconds).GetNextOccurrence(DateTime.UtcNow, TimeZoneInfo.Local)?.ToLocalTime();
    }

    private ISelect<Sys_Job> QueryInternal(QueryReq<QueryJobReq> req)
    {
        return QueryInternal(req, true);
    }

    private ISelect<Sys_Job> QueryInternal(QueryReq<QueryJobReq> req, bool includeUser)
    {
        var ret = Rpo.Select;
        if (includeUser) {
            ret = ret.Include(a => a.User);
        }

        ret = ret.WhereDynamicFilter(req.DynamicFilter)
                 .WhereDynamic(req.Filter)
                 .WhereIf( //
                     req.Keywords?.Length > 0, a => a.Id == req.Keywords.Int64Try(0) || a.JobName.Contains(req.Keywords));

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
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