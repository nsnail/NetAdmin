using FreeSql;
using Furion.FriendlyException;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.DataContract.Attributes;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Role;
using NetAdmin.Infrastructure.Constant;
using NetAdmin.Infrastructure.Lang;

namespace NetAdmin.Application.Service.Sys.Implements;

/// <inheritdoc cref="IRoleService" />
public class RoleService : RepositoryService<TbSysRole, IRoleService>, IRoleService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RoleService" /> class.
    /// </summary>
    public RoleService(Repository<TbSysRole> rpo) //
        : base(rpo) { }

    /// <inheritdoc />
    [Transaction]
    public Task<int> BulkDelete(BulkDelReq req)
    {
        var ret = req.Ids.Sum(x => Delete(new DelReq { Id = x }).Result);
        return Task.FromResult(ret);
    }

    /// <summary>
    ///     创建角色
    /// </summary>
    [Transaction]
    public async Task<QueryRoleRsp> Create(CreateRoleReq @in)
    {
        var entity = @in.Adapt<TbSysRole>();
        var ret    = await Rpo.InsertAsync(entity);

        await Rpo.SaveManyAsync(entity, nameof(entity.Depts));
        await Rpo.SaveManyAsync(entity, nameof(entity.Menus));

        entity.Id = ret.Id;
        return entity.Adapt<QueryRoleRsp>();
    }

    /// <summary>
    ///     删除角色
    /// </summary>
    [Transaction]
    public async Task<int> Delete(DelReq req)
    {
        if (await Rpo.Orm.Select<TbSysUserRole>().ForUpdate().AnyAsync(a => a.RoleId == req.Id)) {
            throw Oops.Oh( //
                Enums.ErrorCodes.InvalidOperation, Str.Users_exist_under_this_role_and_deletion_is_not_allowed);
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
    [Transaction]
    public async Task<QueryRoleRsp> Update(UpdateRoleReq req)
    {
        var entity = req.Adapt<TbSysRole>();
        await Rpo.UpdateAsync(entity);
        await Rpo.SaveManyAsync(entity, nameof(entity.Depts));
        await Rpo.SaveManyAsync(entity, nameof(entity.Menus));

        return entity.Adapt<QueryRoleRsp>();
    }

    private ISelect<TbSysRole> QueryInternal(QueryReq<QueryRoleReq> req)
    {
        var ret = Rpo.Select.IncludeMany(a => a.Depts.Select(b => new TbSysDept { Id = b.Id }))
                     .IncludeMany(a => a.Menus.Select(b => new TbSysMenu { Id        = b.Id }))
                     .WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending)
                     .OrderByIf(req.Prop is not { Length: > 0 }, a => a.Sort, true)
                     .OrderByDescending(a => a.CreatedTime);
        return ret;
    }
}