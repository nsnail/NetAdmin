using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.SiteMsgDept;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ISiteMsgDeptService" />
public sealed class SiteMsgDeptService(BasicRepository<Sys_SiteMsgDept, long> rpo) //
    : RepositoryService<Sys_SiteMsgDept, long, ISiteMsgDeptService>(rpo), ISiteMsgDeptService
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
    public Task<long> CountAsync(QueryReq<QuerySiteMsgDeptReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .CountAsync();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgDeptRsp> CreateAsync(CreateSiteMsgDeptReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QuerySiteMsgDeptRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QuerySiteMsgDeptReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .AnyAsync();
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QuerySiteMsgDeptReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgDeptRsp> GetAsync(QuerySiteMsgDeptReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QuerySiteMsgDeptReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QuerySiteMsgDeptRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QuerySiteMsgDeptRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgDeptReq> req)
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

        return new PagedQueryRsp<QuerySiteMsgDeptRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QuerySiteMsgDeptRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QuerySiteMsgDeptRsp>> QueryAsync(QueryReq<QuerySiteMsgDeptReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req)
                        #if DBTYPE_SQLSERVER
                        .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                        #endif
                        .Take(req.Count)
                        .ToListAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QuerySiteMsgDeptRsp>>();
    }

    private ISelect<Sys_SiteMsgDept> QueryInternal(QueryReq<QuerySiteMsgDeptReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter);

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