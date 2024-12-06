using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Doc.Catalog;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDocCatalogService" />
public sealed class DocCatalogService(BasicRepository<Sys_DocCatalog, long> rpo) //
    : RepositoryService<Sys_DocCatalog, long, IDocCatalogService>(rpo), IDocCatalogService
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
    public Task<long> CountAsync(QueryReq<QueryDocCatalogReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">The_parent_node_does_not_exist</exception>
    public async Task<QueryDocCatalogRsp> CreateAsync(CreateDocCatalogReq req)
    {
        req.ThrowIfInvalid();
        if (req.ParentId != 0 && !await Rpo.Where(a => a.Id == req.ParentId).WithNoLockNoWait().AnyAsync().ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException(Ln.父节点不存在);
        }

        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QueryDocCatalogRsp>();
    }

    /// <inheritdoc />
    public async Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.DeleteCascadeByDatabaseAsync(a => a.Id == req.Id).ConfigureAwait(false);
        return ret.Count;
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">The_parent_node_does_not_exist</exception>
    public async Task<QueryDocCatalogRsp> EditAsync(EditDocCatalogReq req)
    {
        req.ThrowIfInvalid();

        if (req.ParentId != 0 && !await Rpo.Where(a => a.Id == req.ParentId).WithNoLockNoWait().AnyAsync().ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException(Ln.父节点不存在);
        }

        return
            #if DBTYPE_SQLSERVER
            (await UpdateReturnListAsync(req).ConfigureAwait(false)).FirstOrDefault()?.Adapt<QueryDocCatalogRsp>();
            #else
            await UpdateAsync(req).ConfigureAwait(false) > 0 ? await GetAsync(new QueryDocCatalogReq { Id = req.Id }).ConfigureAwait(false) : null;
        #endif
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryDocCatalogReq> req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<QueryDocCatalogRsp> GetAsync(QueryDocCatalogReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryDocCatalogReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryDocCatalogRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryDocCatalogRsp>> PagedQueryAsync(PagedQueryReq<QueryDocCatalogReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).WithNoLockNoWait().Count(out var total).ToListAsync().ConfigureAwait(false);

        return new PagedQueryRsp<QueryDocCatalogRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryDocCatalogRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryDocCatalogRsp>> QueryAsync(QueryReq<QueryDocCatalogReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().ToTreeListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryDocCatalogRsp>>();
    }

    private ISelect<Sys_DocCatalog> QueryInternal(QueryReq<QueryDocCatalogReq> req)
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