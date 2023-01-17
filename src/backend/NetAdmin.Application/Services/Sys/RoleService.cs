using FreeSql;
using Furion.FriendlyException;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.DataContract.DbMaps.Sys;
using NetAdmin.DataContract.Dto.Dependency;
using NetAdmin.DataContract.Dto.Sys.Role;

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
    public async Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await Delete(item);
        }

        return sum;
    }

    /// <summary>
    ///     创建角色
    /// </summary>
    public async Task<QueryRoleRsp> Create(CreateRoleReq req)
    {
        var entity = req.Adapt<TbSysRole>();
        var ret    = await Rpo.InsertAsync(entity);

        await Rpo.SaveManyAsync(entity, nameof(entity.Depts));
        await Rpo.SaveManyAsync(entity, nameof(entity.Menus));
        await Rpo.SaveManyAsync(entity, nameof(entity.Apis));

        entity.Id = ret.Id;
        return entity.Adapt<QueryRoleRsp>();
    }

    /// <summary>
    ///     删除角色
    /// </summary>
    public async Task<int> Delete(DelReq req)
    {
        if (await Rpo.Orm.Select<TbSysUserRole>().ForUpdate().AnyAsync(a => a.RoleId == req.Id)) {
            throw Oops.Oh( //
                Enums.StatusCodes.InvalidOperation, Ln.Users_exist_under_this_role_and_deletion_is_not_allowed);
        }

        var ret = await Rpo.DeleteAsync(a => a.Id == req.Id);
        return ret;
    }

    /// <summary>
    ///     分页查询角色
    /// </summary>
    public async Task<PagedQueryRsp<QueryRoleRsp>> PagedQuery(PagedQueryReq<QueryRoleReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryRoleRsp>(req.Page, req.PageSize, total
                                             , list.Select(x => x.Adapt<QueryRoleRsp>()));
    }

    /// <summary>
    ///     查询角色
    /// </summary>
    public async Task<List<QueryRoleRsp>> Query(QueryReq<QueryRoleReq> req)
    {
        var ret = await QueryInternal(req).ToListAsync();
        return ret.ConvertAll(x => x.Adapt<QueryRoleRsp>());
    }

    /// <summary>
    ///     更新角色
    /// </summary>
    public async Task<QueryRoleRsp> Update(UpdateRoleReq req)
    {
        var entity = req.Adapt<TbSysRole>();
        await Rpo.UpdateAsync(entity);
        await Rpo.SaveManyAsync(entity, nameof(entity.Depts));
        await Rpo.SaveManyAsync(entity, nameof(entity.Menus));
        await Rpo.SaveManyAsync(entity, nameof(entity.Apis));

        return entity.Adapt<QueryRoleRsp>();
    }

    private ISelect<TbSysRole> QueryInternal(QueryReq<QueryRoleReq> req)
    {
        var ret = Rpo.Select.IncludeMany(a => a.Depts.Select(b => new TbSysDept { Id = b.Id }))
                     .IncludeMany(a => a.Menus.Select(b => new TbSysMenu { Id        = b.Id }))
                     .IncludeMany(a => a.Apis.Select(b => new TbSysApi { Id          = b.Id }))
                     .WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending)
                     .OrderByIf(req.Prop is not { Length: > 0 }, a => a.Sort, true)
                     .OrderByDescending(a => a.CreatedTime);
        return ret;
    }
}