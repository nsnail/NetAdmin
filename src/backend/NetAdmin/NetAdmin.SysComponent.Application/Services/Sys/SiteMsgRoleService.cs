using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.SiteMsgRole;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ISiteMsgRoleService" />
public sealed class SiteMsgRoleService(BasicRepository<Sys_SiteMsgRole, long> rpo) //
    : RepositoryService<Sys_SiteMsgRole, long, ISiteMsgRoleService>(rpo), ISiteMsgRoleService
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
    public Task<long> CountAsync(QueryReq<QuerySiteMsgRoleReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgRoleRsp> CreateAsync(CreateSiteMsgRoleReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QuerySiteMsgRoleRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QuerySiteMsgRoleReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().AnyAsync();
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QuerySiteMsgRoleReq> req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgRoleRsp> GetAsync(QuerySiteMsgRoleReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QuerySiteMsgRoleReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QuerySiteMsgRoleRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QuerySiteMsgRoleRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgRoleReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).WithNoLockNoWait().Count(out var total).ToListAsync().ConfigureAwait(false);

        return new PagedQueryRsp<QuerySiteMsgRoleRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QuerySiteMsgRoleRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QuerySiteMsgRoleRsp>> QueryAsync(QueryReq<QuerySiteMsgRoleReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().Take(req.Count).ToListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QuerySiteMsgRoleRsp>>();
    }

    private ISelect<Sys_SiteMsgRole> QueryInternal(QueryReq<QuerySiteMsgRoleReq> req)
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