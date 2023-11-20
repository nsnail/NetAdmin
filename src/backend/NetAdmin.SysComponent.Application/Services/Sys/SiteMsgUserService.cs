using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.SiteMsgUser;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using DataType = FreeSql.DataType;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ISiteMsgUserService" />
public sealed class SiteMsgUserService(DefaultRepository<Sys_SiteMsgUser> rpo) //
    : RepositoryService<Sys_SiteMsgUser, ISiteMsgUserService>(rpo), ISiteMsgUserService
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
    public async Task<QuerySiteMsgUserRsp> CreateAsync(CreateSiteMsgUserReq req)
    {
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QuerySiteMsgUserRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QuerySiteMsgUserReq> req)
    {
        return QueryInternal(req).AnyAsync();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgUserRsp> GetAsync(QuerySiteMsgUserReq req)
    {
        var ret = await QueryInternal(new QueryReq<QuerySiteMsgUserReq> { Filter = req })
                        .ToOneAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<QuerySiteMsgUserRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QuerySiteMsgUserRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgUserReq> req)
    {
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .Count(out var total)
                         .ToListAsync()
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QuerySiteMsgUserRsp>(req.Page, req.PageSize, total
                                                    , list.Adapt<IEnumerable<QuerySiteMsgUserRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QuerySiteMsgUserRsp>> QueryAsync(QueryReq<QuerySiteMsgUserReq> req)
    {
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QuerySiteMsgUserRsp>>();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgUserRsp> UpdateAsync(UpdateSiteMsgUserReq req)
    {
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqliteAsync(req).ConfigureAwait(false) as QuerySiteMsgUserRsp;
        }

        var ret = await Rpo.UpdateDiy.SetSource(req).ExecuteUpdatedAsync().ConfigureAwait(false);
        return ret.FirstOrDefault()?.Adapt<QuerySiteMsgUserRsp>();
    }

    /// <inheritdoc />
    protected override async Task<Sys_SiteMsgUser> UpdateForSqliteAsync(Sys_SiteMsgUser req)
    {
        return await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync().ConfigureAwait(false) <= 0
            ? null
            : await GetAsync(new QuerySiteMsgUserReq { Id = req.Id }).ConfigureAwait(false);
    }

    private ISelect<Sys_SiteMsgUser> QueryInternal(QueryReq<QuerySiteMsgUserReq> req)
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