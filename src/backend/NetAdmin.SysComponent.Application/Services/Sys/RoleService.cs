using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Role;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IRoleService" />
public sealed class RoleService : RepositoryService<Sys_Role, IRoleService>, IRoleService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RoleService" /> class.
    /// </summary>
    public RoleService(Repository<Sys_Role> rpo) //
        : base(rpo) { }

    /// <summary>
    ///     批量删除角色
    /// </summary>
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await DeleteAsync(item);
        }

        return sum;
    }

    /// <summary>
    ///     创建角色
    /// </summary>
    public async Task<QueryRoleRsp> CreateAsync(CreateRoleReq req)
    {
        var entity = req.Adapt<Sys_Role>();
        var ret    = await Rpo.InsertAsync(entity);

        await Rpo.SaveManyAsync(entity, nameof(entity.Depts));
        await Rpo.SaveManyAsync(entity, nameof(entity.Menus));
        await Rpo.SaveManyAsync(entity, nameof(entity.Apis));

        entity = entity with { Id = ret.Id };
        return entity.Adapt<QueryRoleRsp>();
    }

    /// <summary>
    ///     删除角色
    /// </summary>
    /// <exception cref="NetAdminInvalidOperationException">Users_exist_under_this_role_and_deletion_is_not_allowed</exception>
    public async Task<int> DeleteAsync(DelReq req)
    {
        return await Rpo.Orm.Select<Sys_UserRole>().ForUpdate().AnyAsync(a => a.RoleId == req.Id)
            ? throw new NetAdminInvalidOperationException(Ln.该角色下存在用户)
            : await Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <summary>
    ///     判断角色是否存在
    /// </summary>
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<bool> ExistAsync(QueryReq<QueryRoleReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     获取单个角色
    /// </summary>
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<QueryRoleRsp> GetAsync(QueryRoleReq req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     分页查询角色
    /// </summary>
    public async Task<PagedQueryRsp<QueryRoleRsp>> PagedQueryAsync(PagedQueryReq<QueryRoleReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryRoleRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryRoleRsp>>());
    }

    /// <summary>
    ///     查询角色
    /// </summary>
    public async Task<IEnumerable<QueryRoleRsp>> QueryAsync(QueryReq<QueryRoleReq> req)
    {
        var ret = await QueryInternal(req).ToListAsync();
        return ret.Adapt<IEnumerable<QueryRoleRsp>>();
    }

    /// <summary>
    ///     更新角色
    /// </summary>
    public async Task<QueryRoleRsp> UpdateAsync(UpdateRoleReq req)
    {
        var entity = req.Adapt<Sys_Role>();
        _ = await Rpo.UpdateAsync(entity);
        await Rpo.SaveManyAsync(entity, nameof(entity.Depts));
        await Rpo.SaveManyAsync(entity, nameof(entity.Menus));
        await Rpo.SaveManyAsync(entity, nameof(entity.Apis));

        return entity.Adapt<QueryRoleRsp>();
    }

    private ISelect<Sys_Role> QueryInternal(QueryReq<QueryRoleReq> req)
    {
        var ret = Rpo.Select.IncludeMany(a => a.Depts.Select(b => new Sys_Dept { Id = b.Id }))
                     .IncludeMany(a => a.Menus.Select(b => new Sys_Menu { Id        = b.Id }))
                     .IncludeMany(a => a.Apis.Select(b => new Sys_Api { Id          = b.Id }))
                     .WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);

        if (!req.Prop?.Equals(nameof(req.Filter.Sort), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Sort);
        }

        if (!req.Prop?.Equals(nameof(req.Filter.CreatedTime), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.CreatedTime);
        }

        return ret;
    }
}