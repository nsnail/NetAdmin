using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.SiteMsg;
using NetAdmin.Domain.Dto.Sys.SiteMsgFlag;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Enums.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ISiteMsgService" />
public sealed class SiteMsgService(DefaultRepository<Sys_SiteMsg> rpo, ContextUserInfo contextUserInfo
                                 , ISiteMsgFlagService            siteMsgFlagService) //
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
        await CreateUpdateCheckAsync(req);

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
    public async Task<int> DeleteAsync(DelReq req)
    {
        var ret = await Rpo.DeleteCascadeByDatabaseAsync(a => a.Id == req.Id);
        return ret.Count;
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
    public async Task<QuerySiteMsgRsp> GetMineAsync(QuerySiteMsgReq req)
    {
        var ret = await PagedQueryMineAsync(
            new PagedQueryReq<QuerySiteMsgReq> {
                                                   DynamicFilter
                                                       = new DynamicFilterInfo {
                                                                                   Field    = nameof(req.Id)
                                                                                 , Value    = req.Id
                                                                                 , Operator = DynamicFilterOperators.Eq
                                                                               }
                                               }, true);
        return ret.Rows.FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QuerySiteMsgRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QuerySiteMsgRsp>(req.Page, req.PageSize, total
                                                , list.Adapt<IEnumerable<QuerySiteMsgRsp>>());
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QuerySiteMsgRsp>> PagedQueryMineAsync(PagedQueryReq<QuerySiteMsgReq> req)
    {
        return PagedQueryMineAsync(req, false);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QuerySiteMsgRsp>> QueryAsync(QueryReq<QuerySiteMsgReq> req)
    {
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync();
        return ret.Adapt<IEnumerable<QuerySiteMsgRsp>>();
    }

    /// <inheritdoc />
    public async Task SetSiteMsgStatusAsync(UpdateSiteMsgFlagReq req)
    {
        if (!await ExistAsync(new QueryReq<QuerySiteMsgReq> { Filter = new QuerySiteMsgReq { Id = req.SiteMsgId } })) {
            throw new NetAdminInvalidOperationException(Ln.站内信不存在);
        }

        try {
            _ = await siteMsgFlagService.CreateAsync(req with { UserId = contextUserInfo.Id });
        }
        catch {
            _ = await siteMsgFlagService.UpdateAsync(req with { UserId = contextUserInfo.Id });
        }
    }

    /// <inheritdoc />
    public async Task<long> UnreadCountAsync()
    {
        // 减去标记已读的数量
        var subtract = await Rpo.Orm.Select<Sys_SiteMsgFlag>()
                                .Where(a => a.UserId            == contextUserInfo.Id &&
                                            a.UserSiteMsgStatus == UserSiteMsgStatues.Read)
                                .CountAsync();

        return await QueryMineInternal(new QueryReq<QuerySiteMsgReq>()).CountAsync() - subtract;
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgRsp> UpdateAsync(UpdateSiteMsgReq req)
    {
        await CreateUpdateCheckAsync(req);

        // 主表
        var entity = req.Adapt<Sys_SiteMsg>();
        _ = await Rpo.UpdateDiy.SetSource(entity).ExecuteAffrowsAsync();

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Roles));

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Users));

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Depts));

        return (await QueryAsync(new QueryReq<QuerySiteMsgReq> { Filter = new QuerySiteMsgReq { Id = req.Id } }))
            .First();
    }

    /// <inheritdoc />
    protected override async Task<Sys_SiteMsg> UpdateForSqliteAsync(Sys_SiteMsg req)
    {
        return await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0
            ? null
            : await GetAsync(new QuerySiteMsgReq { Id = req.Id });
    }

    private async Task CreateUpdateCheckAsync(CreateSiteMsgReq req)
    {
        // 检查角色是否存在
        if (!req.RoleIds.NullOrEmpty()) {
            var roles = await Rpo.Orm.Select<Sys_Role>().Where(a => req.RoleIds.Contains(a.Id)).ToListAsync(a => a.Id);
            if (roles.Count != req.RoleIds.Count) {
                throw new NetAdminInvalidOperationException(Ln.角色不存在);
            }
        }

        if (!req.DeptIds.NullOrEmpty()) {
            // 检查部门是否存在
            var depts = await Rpo.Orm.Select<Sys_Dept>().Where(a => req.DeptIds.Contains(a.Id)).ToListAsync(a => a.Id);
            if (depts.Count != req.DeptIds.Count) {
                throw new NetAdminInvalidOperationException(Ln.部门不存在);
            }
        }

        if (!req.UserIds.NullOrEmpty()) {
            // 检查用户是否存在
            var users = await Rpo.Orm.Select<Sys_User>().Where(a => req.UserIds.Contains(a.Id)).ToListAsync(a => a.Id);
            if (users.Count != req.UserIds.Count) {
                throw new NetAdminInvalidOperationException(Ln.用户不存在);
            }
        }
    }

    private async Task<PagedQueryRsp<QuerySiteMsgRsp>> PagedQueryMineAsync(
        PagedQueryReq<QuerySiteMsgReq> req, bool containsContent)
    {
        var list = await QueryMineInternal(req)
                         .Page(req.Page, req.PageSize)
                         .Count(out var total)
                         .ToListAsync(a => new QuerySiteMsgRsp {
                                                                   Id      = a.Max(a.Value.Item1.Id)
                                                                 , Title   = a.Max(a.Value.Item1.Title)
                                                                 , Summary = a.Max(a.Value.Item1.Summary)
                                                                 , Content
                                                                       = containsContent
                                                                           ? a.Max(a.Value.Item1.Content)
                                                                           : null
                                                                 , CreatedTime = a.Max(a.Value.Item1.CreatedTime)
                                                                 , MyFlags
                                                                       = new QuerySiteMsgFlagRsp {
                                                                             UserSiteMsgStatus
                                                                                 = a.Max(a.Value.Item6
                                                                                     .UserSiteMsgStatus)
                                                                         }
                                                                 , Creator = new QueryUserRsp {
                                                                                 UserName = a.Max(
                                                                                     a.Value.Item2.UserName)
                                                                               , Avatar = a.Max(
                                                                                     a.Value.Item2.Avatar)
                                                                             }
                                                               });
        return new PagedQueryRsp<QuerySiteMsgRsp>(req.Page, req.PageSize, total
                                                , list.Adapt<IEnumerable<QuerySiteMsgRsp>>());
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

    private ISelectGrouping //
        <long, NativeTuple<Sys_SiteMsg, Sys_User, Sys_SiteMsgDept, Sys_SiteMsgRole, Sys_SiteMsgUser, Sys_SiteMsgFlag>>
        QueryMineInternal(QueryReq<QuerySiteMsgReq> req)
    {
        var roleIds = contextUserInfo.Roles.Select(x => x.Id).ToList();

        return Rpo.Orm
                  .Select<Sys_SiteMsg, Sys_User, Sys_SiteMsgDept, Sys_SiteMsgRole, Sys_SiteMsgUser, Sys_SiteMsgFlag>()
                  .LeftJoin((a, b, _, _, _, _) => a.CreatedUserId == b.Id)
                  .LeftJoin((a, _, c, _, _, _) => a.Id            == c.SiteMsgId)
                  .LeftJoin((a, _, _, d, _, _) => a.Id            == d.SiteMsgId)
                  .LeftJoin((a, _, _, _, e, _) => a.Id            == e.SiteMsgId)
                  .LeftJoin((a, _, _, _, _, f) => a.Id            == f.SiteMsgId)
                  .WhereDynamicFilter(req.DynamicFilter)
                  .Where((a, _, c, d, e, f) =>
                             (SqlExt.EqualIsNull(f.UserSiteMsgStatus) ||
                              f.UserSiteMsgStatus != UserSiteMsgStatues.Deleted) &&
                             (a.MsgType == SiteMsgTypes.Public || c.DeptId == contextUserInfo.DeptId ||
                              roleIds.Contains(d.RoleId)       || e.UserId == contextUserInfo.Id))
                  .GroupBy((a, _, _, _, _, _) => a.Id)
                  .OrderByDescending(a => a.Value.Item1.CreatedTime);
    }
}