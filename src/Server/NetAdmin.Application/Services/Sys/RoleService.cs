using Furion.FriendlyException;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Role;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="IRoleService" />
public class RoleService : RepositoryService<TbSysRole, IRoleService>, IRoleService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RoleService" /> class.
    /// </summary>
    public RoleService(Repository<TbSysRole> rpo) //
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
        var entity = req.Adapt<TbSysRole>();
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
    /// <exception cref="AppFriendlyException">AppFriendlyException</exception>
    public async Task<int> DeleteAsync(DelReq req)
    {
        return await Rpo.Orm.Select<TbSysUserRole>().ForUpdate().AnyAsync(a => a.RoleId == req.Id)
            ? throw Oops.Oh( //
                Enums.RspCodes.InvalidOperation, Ln.Users_exist_under_this_role_and_deletion_is_not_allowed)
            : await Rpo.DeleteAsync(a => a.Id == req.Id);
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
        var entity = req.Adapt<TbSysRole>();
        _ = await Rpo.UpdateAsync(entity);
        await Rpo.SaveManyAsync(entity, nameof(entity.Depts));
        await Rpo.SaveManyAsync(entity, nameof(entity.Menus));
        await Rpo.SaveManyAsync(entity, nameof(entity.Apis));

        return entity.Adapt<QueryRoleRsp>();
    }

    private ISelect<TbSysRole> QueryInternal(QueryReq<QueryRoleReq> req)
    {
        return Rpo.Select.IncludeMany(a => a.Depts.Select(b => new TbSysDept { Id = b.Id }))
                  .IncludeMany(a => a.Menus.Select(b => new TbSysMenu { Id        = b.Id }))
                  .IncludeMany(a => a.Apis.Select(b => new TbSysApi { Id          = b.Id }))
                  .WhereDynamicFilter(req.DynamicFilter)
                  .WhereDynamic(req.Filter)
                  .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending)
                  .OrderByIf(req.Prop is not { Length: > 0 }, a => a.Sort, true)
                  .OrderByDescending(a => a.CreatedTime);
    }
}