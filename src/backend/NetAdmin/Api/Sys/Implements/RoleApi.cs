using FreeSql;
using Furion.FriendlyException;
using Mapster;
using NetAdmin.Aop.Attributes;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Role;
using NetAdmin.Infrastructure.Constant;
using NetAdmin.Lang;
using NetAdmin.Repositories;

namespace NetAdmin.Api.Sys.Implements;

/// <inheritdoc cref="IRoleApi" />
public class RoleApi : RepositoryApi<TbSysRole, IRoleApi>, IRoleApi
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RoleApi" /> class.
    /// </summary>
    public RoleApi(Repository<TbSysRole> repo) //
        : base(repo) { }

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
    public async Task<QueryRoleRsp> Create(CreateRoleReq req)
    {
        var entity = req.Adapt<TbSysRole>();
        var ret    = await Repo.InsertAsync(entity);

        await Repo.SaveManyAsync(entity, nameof(entity.Depts));
        await Repo.SaveManyAsync(entity, nameof(entity.Menus));

        entity.Id = ret.Id;
        return entity.Adapt<QueryRoleRsp>();
    }

    /// <summary>
    ///     删除角色
    /// </summary>
    [Transaction]
    public async Task<int> Delete(DelReq req)
    {
        if (await Repo.Orm.Select<TbSysUserRole>().ForUpdate().AnyAsync(a => a.RoleId == req.Id)) {
            throw Oops.Oh( //
                Enums.ErrorCodes.InvalidOperation, Str.Users_exist_under_this_role_and_deletion_is_not_allowed);
        }

        var ret = await Repo.DeleteAsync(a => a.Id == req.Id);
        return ret;
    }

    /// <inheritdoc />
    [Transaction]
    public async Task<int> MapEndpoints(MapEndpointsReq req)
    {
        if (!await Repo.Select.AnyAsync(a => a.Id == req.RoleId)) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.The_character_id_does_not_exist);
        }

        if (await Repo.Orm.Select<TbSysEndpoint>().Where(a => req.Paths.Contains(a.Path)).CountAsync() !=
            req.Paths.Count) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.Contains_endpoint_paths_that_do_not_exist);
        }

        // 删除原有端点映射
        await Repo.Orm.Delete<TbSysRoleEndpoint>().Where(a => a.RoleId == req.RoleId).ExecuteAffrowsAsync();

        // 插入新的端点映射
        var inserts = req.Paths.Select(x => new TbSysRoleEndpoint { RoleId = req.RoleId, Path = x });
        var ret     = await Repo.Orm.Insert(inserts).ExecuteAffrowsAsync();
        return ret;
    }

    /// <inheritdoc />
    [Transaction]
    public async Task<int> MapMenus(MapMenusReq req)
    {
        if (!await Repo.Select.AnyAsync(a => a.Id == req.RoleId)) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.The_character_id_does_not_exist);
        }

        if (await Repo.Orm.Select<TbSysMenu>().Where(a => req.MenuIds.Contains(a.Id)).CountAsync() !=
            req.MenuIds.Count) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.Contains_a_menu_that_does_not_exist);
        }

        // 删除原有菜单映射
        await Repo.Orm.Delete<TbSysRoleMenu>().Where(a => a.RoleId == req.RoleId).ExecuteAffrowsAsync();

        // 插入新的端点映射
        var inserts = req.MenuIds.Select(x => new TbSysRoleMenu { RoleId = req.RoleId, MenuId = x });
        var ret     = await Repo.Orm.Insert(inserts).ExecuteAffrowsAsync();
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
        await Repo.UpdateAsync(entity);
        await Repo.SaveManyAsync(entity, nameof(entity.Depts));
        await Repo.SaveManyAsync(entity, nameof(entity.Menus));

        return entity.Adapt<QueryRoleRsp>();
    }

    private ISelect<TbSysRole> QueryInternal(QueryReq<QueryRoleReq> req)
    {
        var ret = Repo.Select.IncludeMany(a => a.Depts.Select(b => new TbSysDept { Id = b.Id }))
                      .IncludeMany(a => a.Menus.Select(b => new TbSysMenu { Id        = b.Id }))
                      .WhereDynamicFilter(req.DynamicFilter)
                      .WhereDynamic(req.Filter)
                      .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending)
                      .OrderByIf(req.Prop is not { Length: > 0 }, a => a.Sort, true)
                      .OrderByDescending(a => a.CreatedTime);
        return ret;
    }
}