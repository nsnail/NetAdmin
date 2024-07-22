using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Tpl;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Tpl.Example;
using NetAdmin.SysComponent.Application.Services.Tpl.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Tpl;

/// <inheritdoc cref="IExampleService" />
public sealed class ExampleService(BasicRepository<Tpl_Example, long> rpo) //
    : RepositoryService<Tpl_Example, long, IExampleService>(rpo), IExampleService
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
    public Task<long> CountAsync(QueryReq<QueryExampleReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .CountAsync();
    }

    /// <inheritdoc />
    public async Task<QueryExampleRsp> CreateAsync(CreateExampleReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QueryExampleRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryExampleReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .AnyAsync();
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryExampleReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QueryExampleReq, QueryExampleRsp>(QueryInternal, req, Ln.示例导出);
    }

    /// <inheritdoc />
    public async Task<QueryExampleRsp> GetAsync(QueryExampleReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryExampleReq> { Filter = req })
                        .ToOneAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<QueryExampleRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryExampleRsp>> PagedQueryAsync(PagedQueryReq<QueryExampleReq> req)
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

        return new PagedQueryRsp<QueryExampleRsp>(req.Page, req.PageSize, total
                                                , list.Adapt<IEnumerable<QueryExampleRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryExampleRsp>> QueryAsync(QueryReq<QueryExampleReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req)
                        #if DBTYPE_SQLSERVER
                        .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                        #endif
                        .Take(req.Count)
                        .ToListAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryExampleRsp>>();
    }

    private ISelect<Tpl_Example> QueryInternal(QueryReq<QueryExampleReq> req)
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