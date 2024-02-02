using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Catalog;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDicCatalogService" />
public sealed class DicCatalogService(DefaultRepository<Sys_DicCatalog> rpo) //
    : RepositoryService<Sys_DicCatalog, IDicCatalogService>(rpo), IDicCatalogService
{
    /// <inheritdoc />
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        req.ThrowIfInvalid();
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await DeleteAsync(item).ConfigureAwait(false);
        }

        return sum;
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">The_parent_node_does_not_exist</exception>
    public async Task<QueryDicCatalogRsp> CreateAsync(CreateDicCatalogReq req)
    {
        req.ThrowIfInvalid();
        if (req.ParentId != 0 &&
            !await Rpo.Where(a => a.Id == req.ParentId).ForUpdate().AnyAsync().ConfigureAwait(false)) {
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
    public Task<bool> ExistAsync(QueryReq<QueryDicCatalogReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).AnyAsync();
    }

    /// <inheritdoc />
    public async Task<QueryDicCatalogRsp> GetAsync(QueryDicCatalogReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryDicCatalogReq> { Filter = req })
                        .ToOneAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<QueryDicCatalogRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryDicCatalogRsp>> PagedQueryAsync(PagedQueryReq<QueryDicCatalogReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .Count(out var total)
                         .ToListAsync()
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryDicCatalogRsp>(req.Page, req.PageSize, total
                                                   , list.Adapt<IEnumerable<QueryDicCatalogRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryDicCatalogRsp>> QueryAsync(QueryReq<QueryDicCatalogReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).ToTreeListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryDicCatalogRsp>>();
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">The_parent_node_does_not_exist</exception>
    /// <exception cref="NetAdminUnexpectedException">NetAdminUnexpectedException</exception>
    public async Task<QueryDicCatalogRsp> UpdateAsync(UpdateDicCatalogReq req)
    {
        req.ThrowIfInvalid();
        if (req.ParentId != 0 &&
            !await Rpo.Where(a => a.Id == req.ParentId).ForUpdate().AnyAsync().ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException(Ln.父节点不存在);
        }

        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync().ConfigureAwait(false) <= 0) {
            throw new NetAdminUnexpectedException();
        }

        var ret = await Rpo.Where(a => a.Id == req.Id).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryDicCatalogRsp>();
    }

    /// <inheritdoc />
    protected override Task<Sys_DicCatalog> UpdateForSqliteAsync(Sys_DicCatalog req)
    {
        throw new NotImplementedException();
    }

    private ISelect<Sys_DicCatalog> QueryInternal(QueryReq<QueryDicCatalogReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);
        if (!req.Prop?.Equals(nameof(req.Filter.Id), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Id);
        }

        return ret;
    }
}