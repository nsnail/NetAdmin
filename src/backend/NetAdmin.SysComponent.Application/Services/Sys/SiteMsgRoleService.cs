using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.SiteMsgRole;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using DataType = FreeSql.DataType;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ISiteMsgRoleService" />
public sealed class SiteMsgRoleService(DefaultRepository<Sys_SiteMsgRole> rpo) //
    : RepositoryService<Sys_SiteMsgRole, ISiteMsgRoleService>(rpo), ISiteMsgRoleService
{
    /// <inheritdoc />
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await DeleteAsync(item).ConfigureAwait(false);
        }

        return sum;
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgRoleRsp> CreateAsync(CreateSiteMsgRoleReq req)
    {
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QuerySiteMsgRoleRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QuerySiteMsgRoleReq> req)
    {
        return QueryInternal(req).AnyAsync();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgRoleRsp> GetAsync(QuerySiteMsgRoleReq req)
    {
        var ret = await QueryInternal(new QueryReq<QuerySiteMsgRoleReq> { Filter = req })
                        .ToOneAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<QuerySiteMsgRoleRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QuerySiteMsgRoleRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgRoleReq> req)
    {
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .Count(out var total)
                         .ToListAsync()
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QuerySiteMsgRoleRsp>(req.Page, req.PageSize, total
                                                    , list.Adapt<IEnumerable<QuerySiteMsgRoleRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QuerySiteMsgRoleRsp>> QueryAsync(QueryReq<QuerySiteMsgRoleReq> req)
    {
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QuerySiteMsgRoleRsp>>();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgRoleRsp> UpdateAsync(UpdateSiteMsgRoleReq req)
    {
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqliteAsync(req).ConfigureAwait(false) as QuerySiteMsgRoleRsp;
        }

        var ret = await Rpo.UpdateDiy.SetSource(req).ExecuteUpdatedAsync().ConfigureAwait(false);
        return ret.FirstOrDefault()?.Adapt<QuerySiteMsgRoleRsp>();
    }

    /// <inheritdoc />
    protected override async Task<Sys_SiteMsgRole> UpdateForSqliteAsync(Sys_SiteMsgRole req)
    {
        return await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync().ConfigureAwait(false) <= 0
            ? null
            : await GetAsync(new QuerySiteMsgRoleReq { Id = req.Id }).ConfigureAwait(false);
    }

    private ISelect<Sys_SiteMsgRole> QueryInternal(QueryReq<QuerySiteMsgRoleReq> req)
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