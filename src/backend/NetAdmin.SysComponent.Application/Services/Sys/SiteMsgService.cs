using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.SiteMsg;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ISiteMsgService" />
public sealed class SiteMsgService(DefaultRepository<Sys_SiteMsg> rpo, IUserService userService) //
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
    public async Task<long> UnreadCountAsync()
    {
        var user    = await userService.GetAsync(new QueryUserReq { Id = UserToken.Id });
        var roleIds = user.Roles.Select(x => x.Id).ToList();

        // 1、获取当前用户的私信数量
        var userMsgs = await Rpo.Orm.Select<Sys_SiteMsgUser, Sys_SiteMsg>()
                                .InnerJoin((a,   b) => a.SiteMsgId == b.Id)
                                .Where((a,       _) => a.UserId    == user.Id)
                                .ToListAsync((a, _) => a.SiteMsgId);

        // 2、获取当前用户部门的私信数量
        var deptMsgs = await Rpo.Orm.Select<Sys_SiteMsgDept, Sys_SiteMsg>()
                                .InnerJoin((a,   b) => a.SiteMsgId == b.Id)
                                .Where((a,       _) => a.DeptId    == user.DeptId)
                                .ToListAsync((a, _) => a.SiteMsgId);

        // 3、获取当前用户角色的私信数量
        var roleMsgs = await Rpo.Orm.Select<Sys_SiteMsgRole, Sys_SiteMsg>()
                                .InnerJoin((a,   b) => a.SiteMsgId == b.Id)
                                .Where((a,       _) => roleIds.Contains(a.RoleId))
                                .ToListAsync((a, _) => a.SiteMsgId);

        return userMsgs.Concat(deptMsgs).Concat(roleMsgs).Distinct().Count();
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