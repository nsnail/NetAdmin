using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.LoginLog;
using NetAdmin.Domain.Dto.Sys.RequestLog;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IRequestLogService" />
public sealed class RequestLogService(BasicRepository<Sys_RequestLog, long> rpo, LoginLogService loginLogService) //
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
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryRequestLogReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        .WithNoLockNoWait()
                        .GroupBy(req.GetToListExp<Sys_RequestLog>())
                        .ToDictionaryAsync(a => a.Count())
                        .ConfigureAwait(false);
        return ret.Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                              req.RequiredFields.ToImmutableDictionary(
                                  y => y, y => typeof(Sys_RequestLog).GetProperty(y)!.GetValue(x.Key)!.ToString()), x.Value))
                  .OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<QueryRequestLogRsp> CreateAsync(CreateRequestLogReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);

        // 插入登录日志
        if (req.ApiPathCrc32 == Chars.FLG_PATH_API_SYS_USER_LOGIN_BY_PWD.Crc32()) {
            _ = await loginLogService.CreateAsync(req.Adapt<CreateLoginLogReq>()).ConfigureAwait(false);
        }

        return ret.Adapt<QueryRequestLogRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryRequestLogRsp> EditAsync(EditRequestLogReq req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryRequestLogReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QueryRequestLogReq, ExportRequestLogRsp>( //
            QueryInternal, req, Ln.请求日志导出, a => new {
                                                        a.Id
                                                      , Api = new { a.Api.Id }
                                                      , a.CreatedClientIp
                                                      , a.CreatedTime
                                                      , a.Duration
                                                      , a.HttpMethod
                                                      , a.HttpStatusCode
                                                      , Owner = new { a.Owner.UserName }
                                                    });
    }

    /// <inheritdoc />
    public async Task<QueryRequestLogRsp> GetAsync(QueryRequestLogReq req)
    {
        req.ThrowIfInvalid();
        var df = new DynamicFilterInfo {
                                           Field    = nameof(QueryRequestLogReq.CreatedTime)
                                         , Operator = DynamicFilterOperators.DateRange
                                         , Value = new[] {
                                                             req.CreatedTime.AddHours(-1).yyyy_MM_dd_HH_mm_ss()
                                                           , req.CreatedTime.AddHours(1).yyyy_MM_dd_HH_mm_ss()
                                                         }.Json()
                                                          .Object<JsonElement>()
                                       };
        var ret = await QueryInternal(new QueryReq<QueryRequestLogReq> { Filter = req, DynamicFilter = df, Order = Orders.None })
                        .Include(a => a.Detail)
                        .ToOneAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<QueryRequestLogRsp>();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<GetBarChartRsp>> GetBarChartAsync(QueryReq<QueryRequestLogReq> req)
    {
        req.ThrowIfInvalid();

        var ret = await QueryInternal(req with { Order = Orders.None }, false)
                        .WithNoLockNoWait()
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
    public async Task<IEnumerable<GetPieChartRsp>> GetPieChartByApiSummaryAsync(QueryReq<QueryRequestLogReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        .WithNoLockNoWait()
                        .GroupBy(a => a.Api.Summary)
                        .ToListAsync(a => new GetPieChartRsp { Value = a.Count(), Key = a.Key })
                        .ConfigureAwait(false);
        return ret.OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<GetPieChartRsp>> GetPieChartByHttpStatusCodeAsync(QueryReq<QueryRequestLogReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None }, false)
                        .WithNoLockNoWait()
                        .GroupBy(a => a.HttpStatusCode)
                        #pragma warning disable CA1305
                        .ToListAsync(a => new GetPieChartRsp { Value = a.Count(), Key = a.Key.ToString() })
                        #pragma warning restore CA1305
                        .ConfigureAwait(false);
        return ret.Select(x => x with { Key = Enum.Parse<HttpStatusCode>(x.Key).ToString() }).OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryRequestLogRsp>> PagedQueryAsync(PagedQueryReq<QueryRequestLogReq> req)
    {
        req.ThrowIfInvalid();
        var select     = QueryInternal(req with { Order = Orders.None }, false);
        var selectTemp = select.WithTempQuery(a => new { temp = a });

        if (req.Order == Orders.Random) {
            selectTemp = selectTemp.OrderByRandom();
        }
        else {
            selectTemp = selectTemp.OrderBy( //
                req.Prop?.Length > 0, $"{req.Prop} {(req.Order == Orders.Ascending ? "ASC" : "DESC")}");
            if (!req.Prop?.Equals(nameof(req.Filter.CreatedTime), StringComparison.OrdinalIgnoreCase) ?? true) {
                selectTemp = selectTemp.OrderByDescending(a => a.temp.CreatedTime);
            }
        }

        var ret = await selectTemp.Page(req.Page, req.PageSize)
                                  .WithNoLockNoWait()
                                  .Count(out var total)
                                  .ToListAsync(a => a.temp)
                                  .ConfigureAwait(false);

        return new PagedQueryRsp<QueryRequestLogRsp>(req.Page, req.PageSize, total, ret.Adapt<IEnumerable<QueryRequestLogRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryRequestLogRsp>> QueryAsync(QueryReq<QueryRequestLogReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().Take(req.Count).ToListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryRequestLogRsp>>();
    }

    private ISelect<Sys_RequestLog> QueryInternal(QueryReq<QueryRequestLogReq> req)
    {
        return QueryInternal(req, true);
    }

    private ISelect<Sys_RequestLog> QueryInternal(QueryReq<QueryRequestLogReq> req, bool include)
    {
        var ret = Rpo.Select;
        if (include) {
            ret = ret.Include(a => a.Api).Include(a => a.Owner);
        }

        ret = ret.WhereDynamicFilter(req.DynamicFilter);
        if (req.Filter?.Id is not 0) {
            ret = ret.WhereDynamic(req.Filter);
        }

        if (req.Keywords?.Length > 0) {
            ret = req.Keywords.IsIpV4()
                ? ret.Where(a => a.CreatedClientIp == req.Keywords.IpV4ToInt32())
                : ret.Where(a => a.Id == req.Keywords.Int64Try(0) || a.OwnerId == req.Keywords.Int64Try(0));
        }

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
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