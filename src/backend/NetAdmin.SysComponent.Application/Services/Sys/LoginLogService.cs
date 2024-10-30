using NetAdmin.Domain.Dto.Sys.LoginLog;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ILoginLogService" />
public sealed class LoginLogService(BasicRepository<Sys_LoginLog, long> rpo) //
    : RepositoryService<Sys_LoginLog, long, ILoginLogService>(rpo), ILoginLogService
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
    public Task<long> CountAsync(QueryReq<QueryLoginLogReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .CountAsync();
    }

    /// <inheritdoc />
    public async Task<QueryLoginLogRsp> CreateAsync(CreateLoginLogReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QueryLoginLogRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryLoginLogReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .AnyAsync();
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryLoginLogReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QueryLoginLogReq, ExportLoginLogRsp>(QueryInternal, req, Ln.登录日志导出);
    }

    /// <inheritdoc />
    public async Task<QueryLoginLogRsp> GetAsync(QueryLoginLogReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryLoginLogReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryLoginLogRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryLoginLogRsp>> PagedQueryAsync(PagedQueryReq<QueryLoginLogReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Include(a => a.Owner)
                         .Page(req.Page, req.PageSize)
                         #if DBTYPE_SQLSERVER
                         .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                         #endif
                         .Count(out var total)
                         .ToListAsync(a => new {
                                                   a.CreatedClientIp
                                                 , a.CreatedTime
                                                 , a.CreatedUserAgent
                                                 , a.Duration
                                                 , a.ErrorCode
                                                 , a.HttpStatusCode
                                                 , a.Id
                                                 , a.LoginUserName
                                                 , Owner = new { a.Owner.Id, a.Owner.UserName }
                                                 , a.RequestUrl
                                                 , a.ServerIp
                                               })
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryLoginLogRsp>(req.Page, req.PageSize, total, list.Adapt<List<QueryLoginLogRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryLoginLogRsp>> QueryAsync(QueryReq<QueryLoginLogReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req)
                        #if DBTYPE_SQLSERVER
                        .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                        #endif
                        .Take(req.Count)
                        .ToListAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryLoginLogRsp>>();
    }

    private ISelect<Sys_LoginLog> QueryInternal(QueryReq<QueryLoginLogReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter);

        if (req.Keywords?.Length > 0) {
            ret = req.Keywords.IsIpV4()
                ? ret.Where(a => a.CreatedClientIp == req.Keywords.IpV4ToInt32())
                : ret.Where(a => a.Id == req.Keywords.Int64Try(0) || a.OwnerId == req.Keywords.Int64Try(0) || a.LoginUserName == req.Keywords);
        }

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