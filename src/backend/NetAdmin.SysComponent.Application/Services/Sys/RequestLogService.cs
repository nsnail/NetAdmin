using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.RequestLog;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IRequestLogService" />
public sealed class RequestLogService(BasicRepository<Sys_RequestLog, long> rpo) //
    : RepositoryService<Sys_RequestLog, long, IRequestLogService>(rpo), IRequestLogService
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
    public Task<long> CountAsync(QueryReq<QueryRequestLogReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .CountAsync();
    }

    /// <inheritdoc />
    public async Task<QueryRequestLogRsp> CreateAsync(CreateRequestLogReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QueryRequestLogRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryRequestLogReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .AnyAsync();
    }

    /// <inheritdoc />
    public async Task<QueryRequestLogRsp> GetAsync(QueryRequestLogReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryRequestLogReq> { Filter = req })
                        .ToOneAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<QueryRequestLogRsp>();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<GetBarChartRsp>> GetBarChartAsync(QueryReq<QueryRequestLogReq> req)
    {
        req.ThrowIfInvalid();

        var ret = await QueryInternal(req with { Order = Orders.None })
                        #if DBTYPE_SQLSERVER
                        .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                        #endif
                        .GroupBy(a => new {
                                              a.CreatedTime.Year
                                            , a.CreatedTime.Month
                                            , a.CreatedTime.Day
                                            , a.CreatedTime.Hour
                                          })
                        .ToListAsync(a => new GetBarChartRsp {
                                                                 Timestamp = new DateTime(
                                                                     a.Key.Year, a.Key.Month, a.Key.Day, a.Key.Hour, 0
                                                                   , 0, DateTimeKind.Unspecified)
                                                               , Value = a.Count()
                                                             })
                        .ConfigureAwait(false);
        return ret.OrderBy(x => x.Timestamp);
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<GetPieChartRsp>> GetPieChartByApiSummaryAsync(QueryReq<QueryRequestLogReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        #if DBTYPE_SQLSERVER
                        .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                        #endif
                        .GroupBy(a => a.Api.Summary)
                        .ToListAsync(a => new GetPieChartRsp { Value = a.Count(), Key = a.Key })
                        .ConfigureAwait(false);
        return ret.OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<GetPieChartRsp>> GetPieChartByHttpStatusCodeAsync(
        QueryReq<QueryRequestLogReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        #if DBTYPE_SQLSERVER
                        .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                        #endif
                        .GroupBy(a => a.HttpStatusCode)
                        #pragma warning disable CA1305
                        .ToListAsync(a => new GetPieChartRsp { Value = a.Count(), Key = a.Key.ToString() })
                        #pragma warning restore CA1305
                        .ConfigureAwait(false);
        return ret.Select(x => x with { Key = Enum.Parse<HttpStatusCode>(x.Key).ToString() })
                  .OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryRequestLogRsp>> PagedQueryAsync(PagedQueryReq<QueryRequestLogReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         #if DBTYPE_SQLSERVER
                         .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                         #endif
                         .Count(out var total)
                         .ToListAsync(a => new {
                                                   a.ApiId
                                                 , ApiSummary = a.Api.Summary
                                                 , a.ExtraData
                                                 , a.CreatedClientIp
                                                 , a.CreatedTime
                                                 , a.CreatedUserName
                                                 , a.Duration
                                                 , a.Method
                                                 , a.CreatedUserAgent
                                                 , a.HttpStatusCode
                                                 , a.Id
                                               })
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryRequestLogRsp>(req.Page, req.PageSize, total
                                                   , list.Adapt<IEnumerable<QueryRequestLogRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryRequestLogRsp>> QueryAsync(QueryReq<QueryRequestLogReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req)
                        #if DBTYPE_SQLSERVER
                        .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                        #endif
                        .Take(req.Count)
                        .ToListAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryRequestLogRsp>>();
    }

    private ISelect<Sys_RequestLog> QueryInternal(QueryReq<QueryRequestLogReq> req)
    {
        var ret = Rpo.Select.Include(a => a.Api).WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter);
        switch (req.Order) {
            case Orders.None:
                return ret;
            case Orders.Random:
                return ret.OrderByRandom();
        }

        ret = ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);

        if (!req.Prop?.Equals(nameof(req.Filter.CreatedTime), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.CreatedTime);
        }

        return ret;
    }
}