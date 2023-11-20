using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.SiteMsgDept;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using DataType = FreeSql.DataType;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ISiteMsgDeptService" />
public sealed class SiteMsgDeptService(DefaultRepository<Sys_SiteMsgDept> rpo) //
    : RepositoryService<Sys_SiteMsgDept, ISiteMsgDeptService>(rpo), ISiteMsgDeptService
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
    public async Task<QuerySiteMsgDeptRsp> CreateAsync(CreateSiteMsgDeptReq req)
    {
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QuerySiteMsgDeptRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QuerySiteMsgDeptReq> req)
    {
        return QueryInternal(req).AnyAsync();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgDeptRsp> GetAsync(QuerySiteMsgDeptReq req)
    {
        var ret = await QueryInternal(new QueryReq<QuerySiteMsgDeptReq> { Filter = req })
                        .ToOneAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<QuerySiteMsgDeptRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QuerySiteMsgDeptRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgDeptReq> req)
    {
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .Count(out var total)
                         .ToListAsync()
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QuerySiteMsgDeptRsp>(req.Page, req.PageSize, total
                                                    , list.Adapt<IEnumerable<QuerySiteMsgDeptRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QuerySiteMsgDeptRsp>> QueryAsync(QueryReq<QuerySiteMsgDeptReq> req)
    {
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QuerySiteMsgDeptRsp>>();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgDeptRsp> UpdateAsync(UpdateSiteMsgDeptReq req)
    {
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqliteAsync(req).ConfigureAwait(false) as QuerySiteMsgDeptRsp;
        }

        var ret = await Rpo.UpdateDiy.SetSource(req).ExecuteUpdatedAsync().ConfigureAwait(false);
        return ret.FirstOrDefault()?.Adapt<QuerySiteMsgDeptRsp>();
    }

    /// <inheritdoc />
    protected override async Task<Sys_SiteMsgDept> UpdateForSqliteAsync(Sys_SiteMsgDept req)
    {
        return await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync().ConfigureAwait(false) <= 0
            ? null
            : await GetAsync(new QuerySiteMsgDeptReq { Id = req.Id }).ConfigureAwait(false);
    }

    private ISelect<Sys_SiteMsgDept> QueryInternal(QueryReq<QuerySiteMsgDeptReq> req)
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