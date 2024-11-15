using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.JobRecord;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IJobRecordService" />
public sealed class JobRecordService(BasicRepository<Sys_JobRecord, long> rpo) //
    : RepositoryService<Sys_JobRecord, long, IJobRecordService>(rpo), IJobRecordService
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
    public Task<long> CountAsync(QueryReq<QueryJobRecordReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
               #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
               #endif
               .CountAsync();
    }

    /// <inheritdoc />
    public async Task<QueryJobRecordRsp> CreateAsync(CreateJobRecordReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QueryJobRecordRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryJobRecordReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
               #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
               #endif
               .AnyAsync();
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryJobRecordReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QueryJobRecordReq, ExportJobRecordRsp>(QueryInternal, req, Ln.计划作业执行记录导出);
    }

    /// <inheritdoc />
    public async Task<QueryJobRecordRsp> GetAsync(QueryJobRecordReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryJobRecordReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryJobRecordRsp>();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<GetBarChartRsp>> GetBarChartAsync(QueryReq<QueryJobRecordReq> req)
    {
        req.ThrowIfInvalid();

        var ret = await QueryInternal(req with { Order = Orders.None })
                        #if DBTYPE_SQLSERVER
                        .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                        #endif
                        .GroupBy(a => new { a.CreatedTime.Year, a.CreatedTime.Month, a.CreatedTime.Day, a.CreatedTime.Hour })
                        .ToListAsync(a => new GetBarChartRsp {
                                                                 Timestamp = new DateTime(a.Key.Year, a.Key.Month, a.Key.Day, a.Key.Hour, 0, 0
                                                                                        , DateTimeKind.Unspecified)
                                                               , Value = a.Count()
                                                             })
                        .ConfigureAwait(false);
        return ret.OrderBy(x => x.Timestamp);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<GetPieChartRsp>> GetPieChartByHttpStatusCodeAsync(QueryReq<QueryJobRecordReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        #if DBTYPE_SQLSERVER
                        .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                        #endif
                        .Include(a => a.Job)
                        .GroupBy(a => a.HttpStatusCode)
                        #pragma warning disable CA1305
                        .ToListAsync(a => new GetPieChartRsp { Value = a.Count(), Key = a.Key.ToString() })
                        #pragma warning restore CA1305
                        .ConfigureAwait(false);
        return ret.Select(x => x with { Key = Enum.Parse<HttpStatusCode>(x.Key).ToString() }).OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<GetPieChartRsp>> GetPieChartByNameAsync(QueryReq<QueryJobRecordReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        #if DBTYPE_SQLSERVER
                        .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                        #endif
                        .Include(a => a.Job)
                        .GroupBy(a => a.Job.JobName)
                        .ToListAsync(a => new GetPieChartRsp { Value = a.Count(), Key = a.Key })
                        .ConfigureAwait(false);
        return ret.OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryJobRecordRsp>> PagedQueryAsync(PagedQueryReq<QueryJobRecordReq> req)
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

        return new PagedQueryRsp<QueryJobRecordRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryJobRecordRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryJobRecordRsp>> QueryAsync(QueryReq<QueryJobRecordReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req)
                        #if DBTYPE_SQLSERVER
                        .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                        #endif
                        .Take(req.Count)
                        .ToListAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryJobRecordRsp>>();
    }

    private ISelect<Sys_JobRecord> QueryInternal(QueryReq<QueryJobRecordReq> req)
    {
        var ret = Rpo.Select.Include(a => a.Job)
                     .WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .WhereIf( //
                         req.Keywords?.Length > 0
                       , a => a.JobId == req.Keywords.Int64Try(0) || a.Id == req.Keywords.Int64Try(0) || a.Job.JobName == req.Keywords);

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (req.Order) {
            case Orders.None:
                return ret;
            case Orders.Random:
                return ret.OrderByRandom();
        }

        ret = ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);
        if (!req.Prop?.Equals(nameof(req.Filter.Id), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Id);
        }

        return ret;
    }
}