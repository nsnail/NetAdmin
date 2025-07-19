using NetAdmin.Application.Extensions;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Dic.Catalog;
using NetAdmin.Domain.Extensions;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDicCatalogService" />
public sealed class DicCatalogService(BasicRepository<Sys_DicCatalog, long> rpo) //
    : RepositoryService<Sys_DicCatalog, long, IDicCatalogService>(rpo), IDicCatalogService
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
    public Task<long> CountAsync(QueryReq<QueryDicCatalogReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryDicCatalogReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        .WithNoLockNoWait()
                        .GroupBy(req.GetToListExp<Sys_DicCatalog>())
                        .ToDictionaryAsync(a => a.Count())
                        .ConfigureAwait(false);
        return ret.Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                              req.RequiredFields.ToImmutableDictionary(
                                  y => y, y => typeof(Sys_DicCatalog).GetProperty(y)!.GetValue(x.Key)!.ToString()), x.Value))
                  .OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">The_parent_node_does_not_exist</exception>
    public async Task<QueryDicCatalogRsp> CreateAsync(CreateDicCatalogReq req)
    {
        req.ThrowIfInvalid();
        if (req.ParentId != 0 && !await Rpo.Where(a => a.Id == req.ParentId).WithNoLockNoWait().AnyAsync().ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException(Ln.父节点不存在);
        }

        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QueryDicCatalogRsp>();
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
    public async Task<QueryDicCatalogRsp> EditAsync(EditDicCatalogReq req)
    {
        req.ThrowIfInvalid();
        if (req.ParentId != 0 && !await Rpo.Where(a => a.Id == req.ParentId).WithNoLockNoWait().AnyAsync().ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException(Ln.父节点不存在);
        }

        return
            #if DBTYPE_SQLSERVER
            (await UpdateReturnListAsync(req).ConfigureAwait(false)).FirstOrDefault()?.Adapt<QueryDicCatalogRsp>();
        #else
            await UpdateAsync(req).ConfigureAwait(false) > 0 ? await GetAsync(new QueryDicCatalogReq { Id = req.Id }).ConfigureAwait(false) : null;
        #endif
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryDicCatalogReq> req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<QueryDicCatalogRsp> GetAsync(QueryDicCatalogReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryDicCatalogReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryDicCatalogRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryDicCatalogRsp>> PagedQueryAsync(PagedQueryReq<QueryDicCatalogReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .WithNoLockNoWait()
                         .Count(out var total)
                         .ToListAsync(req)
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryDicCatalogRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryDicCatalogRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryDicCatalogRsp>> QueryAsync(QueryReq<QueryDicCatalogReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().ToTreeListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryDicCatalogRsp>>();
    }

    private ISelect<Sys_DicCatalog> QueryInternal(QueryReq<QueryDicCatalogReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereIf(req.Filter?.Id           > 0, a => a.Id   == req.Filter.Id)
                     .WhereIf(req.Filter?.Code?.Length > 0, a => a.Code == req.Filter.Code);

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (req.Order) {
            case Orders.None:
                return ret.AppendOtherFilters(req);
            case Orders.Random:
                return ret.OrderByRandom();
        }

        ret = ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);
        if (!req.Prop?.Equals(nameof(req.Filter.Id), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Id);
        }

        return ret.AppendOtherFilters(req);
    }
}