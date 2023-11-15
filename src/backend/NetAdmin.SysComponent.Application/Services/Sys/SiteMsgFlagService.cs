using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.SiteMsgFlag;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using DataType = FreeSql.DataType;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ISiteMsgFlagService" />
public sealed class SiteMsgFlagService(DefaultRepository<Sys_SiteMsgFlag> rpo) //
    : RepositoryService<Sys_SiteMsgFlag, ISiteMsgFlagService>(rpo), ISiteMsgFlagService
{
    /// <inheritdoc />
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await DeleteAsync(item);
        }

        return sum;
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgFlagRsp> CreateAsync(CreateSiteMsgFlagReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QuerySiteMsgFlagRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QuerySiteMsgFlagReq> req)
    {
        return QueryInternal(req).AnyAsync();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgFlagRsp> GetAsync(QuerySiteMsgFlagReq req)
    {
        var ret = await QueryInternal(new QueryReq<QuerySiteMsgFlagReq> { Filter = req }).ToOneAsync();
        return ret.Adapt<QuerySiteMsgFlagRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QuerySiteMsgFlagRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgFlagReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QuerySiteMsgFlagRsp>(req.Page, req.PageSize, total
                                                    , list.Adapt<IEnumerable<QuerySiteMsgFlagRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QuerySiteMsgFlagRsp>> QueryAsync(QueryReq<QuerySiteMsgFlagReq> req)
    {
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync();
        return ret.Adapt<IEnumerable<QuerySiteMsgFlagRsp>>();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgFlagRsp> UpdateAsync(UpdateSiteMsgFlagReq req)
    {
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqliteAsync(req) as QuerySiteMsgFlagRsp;
        }

        var ret = await Rpo.UpdateDiy.Set(a => a.UserSiteMsgStatus == req.UserSiteMsgStatus)
                           .Where(a => a.UserId == req.UserId && a.SiteMsgId == req.SiteMsgId)
                           .ExecuteUpdatedAsync();
        return ret.FirstOrDefault()?.Adapt<QuerySiteMsgFlagRsp>();
    }

    /// <inheritdoc />
    protected override async Task<Sys_SiteMsgFlag> UpdateForSqliteAsync(Sys_SiteMsgFlag req)
    {
        return await Rpo.UpdateDiy.Set(a => a.UserSiteMsgStatus == req.UserSiteMsgStatus)
                        .Where(a => a.UserId == req.UserId && a.SiteMsgId == req.SiteMsgId)
                        .ExecuteAffrowsAsync() <= 0
            ? null
            : await GetAsync(new QuerySiteMsgFlagReq { Id = req.Id });
    }

    private ISelect<Sys_SiteMsgFlag> QueryInternal(QueryReq<QuerySiteMsgFlagReq> req)
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