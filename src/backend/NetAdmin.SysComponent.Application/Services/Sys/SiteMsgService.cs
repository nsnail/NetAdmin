using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.SiteMsg;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using DataType = FreeSql.DataType;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ISiteMsgService" />
public sealed class SiteMsgService(DefaultRepository<Sys_SiteMsg> rpo) //
    : RepositoryService<Sys_SiteMsg, ISiteMsgService>(rpo), ISiteMsgService
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
    public async Task<QuerySiteMsgRsp> CreateAsync(CreateSiteMsgReq req)
    {
        // 主表
        var entity    = req.Adapt<Sys_SiteMsg>();
        var dbSiteMsg = await Rpo.InsertAsync(entity);

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Roles));

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Users));

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Depts));

        var ret = await QueryAsync(
            new QueryReq<QuerySiteMsgReq> { Filter = new QuerySiteMsgReq { Id = dbSiteMsg.Id } });

        return ret.Adapt<QuerySiteMsgRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QuerySiteMsgReq> req)
    {
        return QueryInternal(req).AnyAsync();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgRsp> GetAsync(QuerySiteMsgReq req)
    {
        var ret = await QueryInternal(new QueryReq<QuerySiteMsgReq> { Filter = req })
                        .IncludeMany(a => a.Roles)
                        .IncludeMany(a => a.Users)
                        .IncludeMany(a => a.Depts)
                        .ToOneAsync();
        return ret.Adapt<QuerySiteMsgRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QuerySiteMsgRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QuerySiteMsgRsp>(req.Page, req.PageSize, total
                                                , list.Adapt<IEnumerable<QuerySiteMsgRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QuerySiteMsgRsp>> QueryAsync(QueryReq<QuerySiteMsgReq> req)
    {
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync();
        return ret.Adapt<IEnumerable<QuerySiteMsgRsp>>();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgRsp> UpdateAsync(UpdateSiteMsgReq req)
    {
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqliteAsync(req) as QuerySiteMsgRsp;
        }

        var ret = await Rpo.UpdateDiy.SetSource(req).ExecuteUpdatedAsync();
        return ret.FirstOrDefault()?.Adapt<QuerySiteMsgRsp>();
    }

    /// <inheritdoc />
    protected override async Task<Sys_SiteMsg> UpdateForSqliteAsync(Sys_SiteMsg req)
    {
        return await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0
            ? null
            : await GetAsync(new QuerySiteMsgReq { Id = req.Id });
    }

    private ISelect<Sys_SiteMsg> QueryInternal(QueryReq<QuerySiteMsgReq> req)
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