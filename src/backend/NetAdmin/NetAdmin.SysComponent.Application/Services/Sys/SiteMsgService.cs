using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.SiteMsg;
using NetAdmin.Domain.Dto.Sys.SiteMsgFlag;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ISiteMsgService" />
public sealed class SiteMsgService(BasicRepository<Sys_SiteMsg, long> rpo, ContextUserInfo contextUserInfo, ISiteMsgFlagService siteMsgFlagService) //
    : RepositoryService<Sys_SiteMsg, long, ISiteMsgService>(rpo), ISiteMsgService
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
    public Task<long> CountAsync(QueryReq<QuerySiteMsgReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgRsp> CreateAsync(CreateSiteMsgReq req)
    {
        req.ThrowIfInvalid();
        await CreateEditCheckAsync(req).ConfigureAwait(false);

        // 主表
        var entity    = req.Adapt<Sys_SiteMsg>();
        var dbSiteMsg = await Rpo.InsertAsync(entity).ConfigureAwait(false);

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Roles)).ConfigureAwait(false);

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Users)).ConfigureAwait(false);

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Depts)).ConfigureAwait(false);

        var ret = await QueryAsync(new QueryReq<QuerySiteMsgReq> { Filter = new QuerySiteMsgReq { Id = dbSiteMsg.Id } }).ConfigureAwait(false);

        return ret.Adapt<QuerySiteMsgRsp>();
    }

    /// <inheritdoc />
    public async Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.DeleteCascadeByDatabaseAsync(a => a.Id == req.Id).ConfigureAwait(false);
        return ret.Count;
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgRsp> EditAsync(EditSiteMsgReq req)
    {
        req.ThrowIfInvalid();
        await CreateEditCheckAsync(req).ConfigureAwait(false);

        // 主表
        var entity = req.Adapt<Sys_SiteMsg>();
        _ = await UpdateAsync(entity, null).ConfigureAwait(false);

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Roles)).ConfigureAwait(false);

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Users)).ConfigureAwait(false);

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Depts)).ConfigureAwait(false);

        return (await QueryAsync(new QueryReq<QuerySiteMsgReq> { Filter = new QuerySiteMsgReq { Id = req.Id } }).ConfigureAwait(false)).First();
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QuerySiteMsgReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().AnyAsync();
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QuerySiteMsgReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QuerySiteMsgReq, ExportSiteMsgRsp>(QueryInternal, req, Ln.站内信导出);
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgRsp> GetAsync(QuerySiteMsgReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QuerySiteMsgReq> { Filter = req, Order = Orders.None })
                        .IncludeMany(a => a.Roles)
                        .IncludeMany(a => a.Users)
                        .IncludeMany(a => a.Depts)
                        .ToOneAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<QuerySiteMsgRsp>();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgRsp> GetMineAsync(QuerySiteMsgReq req)
    {
        req.ThrowIfInvalid();
        var ret = await PagedQueryMineAsync(
                new PagedQueryReq<QuerySiteMsgReq> {
                                                       DynamicFilter = new DynamicFilterInfo {
                                                                                                 Field    = nameof(req.Id)
                                                                                               , Value    = req.Id
                                                                                               , Operator = DynamicFilterOperators.Eq
                                                                                             }
                                                   }, true)
            .ConfigureAwait(false);
        return ret.Rows.FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QuerySiteMsgRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .WithNoLockNoWait()
                         .Count(out var total)
                         .ToListAsync(a => new {
                                                   a.CreatedTime
                                                 , a.CreatedUserName
                                                 , a.Id
                                                 , a.MsgType
                                                 , a.Summary
                                                 , a.Title
                                                 , a.Version
                                               })
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QuerySiteMsgRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QuerySiteMsgRsp>>());
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QuerySiteMsgRsp>> PagedQueryMineAsync(PagedQueryReq<QuerySiteMsgReq> req)
    {
        req.ThrowIfInvalid();
        return PagedQueryMineAsync(req, false);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QuerySiteMsgRsp>> QueryAsync(QueryReq<QuerySiteMsgReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().Take(req.Count).ToListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QuerySiteMsgRsp>>();
    }

    /// <inheritdoc />
    public async Task SetSiteMsgStatusAsync(SetUserSiteMsgStatusReq req)
    {
        req.ThrowIfInvalid();
        if (!await ExistAsync(new QueryReq<QuerySiteMsgReq> { Filter = new QuerySiteMsgReq { Id = req.SiteMsgId } }).ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException(Ln.站内信不存在);
        }

        try {
            _ = await siteMsgFlagService.CreateAsync(req with { UserId = contextUserInfo.Id }).ConfigureAwait(false);
        }
        catch {
            await siteMsgFlagService.SetUserSiteMsgStatusAsync(req with { UserId = contextUserInfo.Id }).ConfigureAwait(false);
        }
    }

    /// <inheritdoc />
    public async Task<long> UnreadCountAsync()
    {
        // 减去标记已读的数量
        var subtract = await Rpo.Orm.Select<Sys_SiteMsgFlag>()
                                .Where(a => a.UserId == contextUserInfo.Id && a.UserSiteMsgStatus == UserSiteMsgStatues.Read)
                                .CountAsync()
                                .ConfigureAwait(false);

        return await QueryMineInternal(new QueryReq<QuerySiteMsgReq>()).CountAsync().ConfigureAwait(false) - subtract;
    }

    private async Task CreateEditCheckAsync(CreateSiteMsgReq req)
    {
        // 检查角色是否存在
        if (!req.RoleIds.NullOrEmpty()) {
            var roles = await Rpo.Orm.Select<Sys_Role>().Where(a => req.RoleIds.Contains(a.Id)).ToListAsync(a => a.Id).ConfigureAwait(false);
            if (roles.Count != req.RoleIds.Count) {
                throw new NetAdminInvalidOperationException(Ln.角色不存在);
            }
        }

        if (!req.DeptIds.NullOrEmpty()) {
            // 检查部门是否存在
            var depts = await Rpo.Orm.Select<Sys_Dept>().Where(a => req.DeptIds.Contains(a.Id)).ToListAsync(a => a.Id).ConfigureAwait(false);
            if (depts.Count != req.DeptIds.Count) {
                throw new NetAdminInvalidOperationException(Ln.部门不存在);
            }
        }

        if (!req.UserIds.NullOrEmpty()) {
            // 检查用户是否存在
            var users = await Rpo.Orm.Select<Sys_User>().Where(a => req.UserIds.Contains(a.Id)).ToListAsync(a => a.Id).ConfigureAwait(false);
            if (users.Count != req.UserIds.Count) {
                throw new NetAdminInvalidOperationException(Ln.用户不存在);
            }
        }
    }

    private async Task<PagedQueryRsp<QuerySiteMsgRsp>> PagedQueryMineAsync(PagedQueryReq<QuerySiteMsgReq> req, bool containsContent)
    {
        var list = await QueryMineInternal(req)
                         .Page(req.Page, req.PageSize)
                         .Count(out var total)
                         .OrderByDescending(a => a.Max(a.Value.Item1.CreatedTime))
                         .ToListAsync(a => new QuerySiteMsgRsp {
                                                                   Id          = a.Max(a.Value.Item1.Id)
                                                                 , Title       = a.Max(a.Value.Item1.Title)
                                                                 , Summary     = a.Max(a.Value.Item1.Summary)
                                                                 , Content     = containsContent ? a.Max(a.Value.Item1.Content) : null
                                                                 , CreatedTime = a.Max(a.Value.Item1.CreatedTime)
                                                                 , MyFlags
                                                                       = new QuerySiteMsgFlagRsp {
                                                                                                     UserSiteMsgStatus
                                                                                                         = a.Max(a.Value.Item6.UserSiteMsgStatus)
                                                                                                 }
                                                                 , Sender = new QueryUserRsp {
                                                                                                 UserName = a.Max(a.Value.Item2.UserName)
                                                                                               , Avatar   = a.Max(a.Value.Item2.Avatar)
                                                                                             }
                                                               })
                         .ConfigureAwait(false);
        return new PagedQueryRsp<QuerySiteMsgRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QuerySiteMsgRsp>>());
    }

    private ISelect<Sys_SiteMsg> QueryInternal(QueryReq<QuerySiteMsgReq> req)
    {
        var ret = Rpo.Select.Include(a => a.Creator)
                     .WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .WhereIf( //
                         req.Keywords?.Length > 0
                       , a => a.Id == req.Keywords.Int64Try(0) || a.Title.Contains(req.Keywords) || a.Summary.Contains(req.Keywords));

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

    private ISelectGrouping //
        <long, NativeTuple<Sys_SiteMsg, Sys_User, Sys_SiteMsgDept, Sys_SiteMsgRole, Sys_SiteMsgUser, Sys_SiteMsgFlag>> QueryMineInternal(
            QueryReq<QuerySiteMsgReq> req)
    {
        var roleIds = contextUserInfo.Roles.Select(x => x.Id).ToList();

        return Rpo.Orm.Select<Sys_SiteMsg, Sys_User, Sys_SiteMsgDept, Sys_SiteMsgRole, Sys_SiteMsgUser, Sys_SiteMsgFlag>()
                  .WithNoLockNoWait()
                  .LeftJoin((a, b, _, _, _, _) => a.CreatedUserId == b.Id)
                  .LeftJoin((a, _, c, _, _, _) => a.Id            == c.SiteMsgId)
                  .LeftJoin((a, _, _, d, _, _) => a.Id            == d.SiteMsgId)
                  .LeftJoin((a, _, _, _, e, _) => a.Id            == e.SiteMsgId)
                  .LeftJoin((a, _, _, _, _, f) => a.Id == f.SiteMsgId && f.UserId == contextUserInfo.Id)
                  .WhereDynamicFilter(req.DynamicFilter)
                  .Where((a, _, c, d, e, f) => (SqlExt.EqualIsNull(f.UserSiteMsgStatus) || f.UserSiteMsgStatus != UserSiteMsgStatues.Deleted) &&
                                               (a.MsgType == SiteMsgTypes.Public || c.DeptId == contextUserInfo.DeptId ||
                                                roleIds.Contains(d.RoleId)       || e.UserId == contextUserInfo.Id))
                  .GroupBy((a, _, _, _, _, _) => a.Id);
    }
}