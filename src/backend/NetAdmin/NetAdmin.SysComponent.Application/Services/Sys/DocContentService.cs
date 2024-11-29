using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Doc.Content;
using NetAdmin.Domain.Enums.Sys;
#if !DBTYPE_SQLSERVER
using NetAdmin.Domain.DbMaps.Dependency.Fields;
#endif

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDocContentService" />
public sealed class DocContentService(BasicRepository<Sys_DocContent, long> rpo) //
    : RepositoryService<Sys_DocContent, long, IDocContentService>(rpo), IDocContentService
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
    public Task<long> CountAsync(QueryReq<QueryDocContentReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">Doctionary_directory_does_not_exist</exception>
    public async Task<QueryDocContentRsp> CreateAsync(CreateDocContentReq req)
    {
        req.ThrowIfInvalid();
        if (!await Rpo.Orm.Select<Sys_DocCatalog>().Where(a => a.Id == req.CatalogId).WithNoLockNoWait().AnyAsync().ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException(Ln.文档分类不存在);
        }

        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QueryDocContentRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">Doctionary_directory_does_not_exist</exception>
    public async Task<QueryDocContentRsp> EditAsync(EditDocContentReq req)
    {
        req.ThrowIfInvalid();
        if (!await Rpo.Orm.Select<Sys_DocCatalog>().Where(a => a.Id == req.CatalogId).WithNoLockNoWait().AnyAsync().ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException(Ln.文档分类不存在);
        }

        #if DBTYPE_SQLSERVER
        return (await UpdateReturnListAsync(req, null).ConfigureAwait(false)).FirstOrDefault()?.Adapt<QueryDocContentRsp>();
        #else
        return await UpdateAsync(req, null, [nameof(IFieldOwner.OwnerId), nameof(IFieldOwner.OwnerDeptId)]).ConfigureAwait(false) > 0
            ? await GetAsync(new QueryDocContentReq { Id = req.Id }).ConfigureAwait(false)
            : null;
        #endif
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryDocContentReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().AnyAsync();
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryDocContentReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QueryDocContentReq, ExportDocContentRsp>(QueryInternal, req, Ln.文档内容导出);
    }

    /// <inheritdoc />
    public async Task<QueryDocContentRsp> GetAsync(QueryDocContentReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryDocContentReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryDocContentRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryDocContentRsp>> PagedQueryAsync(PagedQueryReq<QueryDocContentReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).WithNoLockNoWait().Count(out var total).ToListAsync().ConfigureAwait(false);

        return new PagedQueryRsp<QueryDocContentRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryDocContentRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryDocContentRsp>> QueryAsync(QueryReq<QueryDocContentReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().Take(req.Count).ToListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryDocContentRsp>>();
    }

    /// <inheritdoc />
    public Task<int> SetEnabledAsync(SetDocContentEnabledReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req, [nameof(Sys_DocContent.Enabled)]);
    }

    /// <inheritdoc />
    public async Task<QueryDocContentRsp> ViewAsync(QueryDocContentReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryDocContentReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);

        switch (ret?.Visibility) {
            case ArchiveVisibilities.LogonUser:
                if (UserToken == null) {
                    return null;
                }

                break;
            case ArchiveVisibilities.DeptUser:
                if (UserToken == null || UserToken.DeptId != ret.OwnerDeptId) {
                    return null;
                }

                break;
            case ArchiveVisibilities.Self:
                if (UserToken == null || UserToken.Id != ret.OwnerId) {
                    return null;
                }

                break;
        }

        return ret?.Enabled == false ? null : ret?.Adapt<QueryDocContentRsp>();
    }

    private ISelect<Sys_DocContent> QueryInternal(QueryReq<QueryDocContentReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .WhereIf( //
                         req.Keywords?.Length > 0, a => a.Title.Contains(req.Keywords));

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