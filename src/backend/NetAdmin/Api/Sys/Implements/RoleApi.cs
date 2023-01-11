using Furion.FriendlyException;
using Mapster;
using NetAdmin.Aop.Attributes;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Role;
using NetAdmin.Infrastructure.Constant;
using NetAdmin.Lang;
using NetAdmin.Repositories;
using NSExt.Extensions;

namespace NetAdmin.Api.Sys.Implements;

/// <inheritdoc cref="IRoleApi" />
public class RoleApi : RepositoryApi<TbSysRole, IRoleApi>, IRoleApi
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RoleApi" /> class.
    /// </summary>
    public RoleApi(Repository<TbSysRole> repository) //
        : base(repository) { }

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
        var ret = await Repository.InsertAsync(req);

        if (req.DataScope == Enums.DataScopes.SpecificDept) {
            _ = await MapDataScopeWithDept(ret.Id, req.DpetIdsInDataScope);
        }

        return ret.Adapt<QueryRoleRsp>();
    }

    /// <summary>
    ///     删除角色
    /// </summary>
    [Transaction]
    public async Task<int> Delete(DelReq req)
    {
        if (await Repository.Orm.Select<TbSysUserRole>().ForUpdate().AnyAsync(a => a.RoleId == req.Id)) {
            throw Oops.Oh( //
                Enums.ErrorCodes.InvalidOperation, Str.Users_exist_under_this_role_and_deletion_is_not_allowed);
        }

        var ret = await Repository.DeleteAsync(a => a.Id == req.Id);
        return ret;
    }

    /// <inheritdoc />
    [Transaction]
    public async Task<int> MapEndpoints(MapEndpointsReq req)
    {
        if (!await Repository.Select.AnyAsync(a => a.Id == req.RoleId)) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.The_character_id_does_not_exist);
        }

        if (await Repository.Orm.Select<TbSysEndpoint>().Where(a => req.Paths.Contains(a.Path)).CountAsync() !=
            req.Paths.Count) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.Contains_endpoint_paths_that_do_not_exist);
        }

        //删除原有端点映射
        await Repository.Orm.Delete<TbSysRoleEndpoint>().Where(a => a.RoleId == req.RoleId).ExecuteAffrowsAsync();

        //插入新的端点映射
        var inserts = req.Paths.Select(x => new TbSysRoleEndpoint { RoleId = req.RoleId, Path = x });
        var ret     = await Repository.Orm.Insert(inserts).ExecuteAffrowsAsync();
        return ret;
    }

    /// <inheritdoc />
    [Transaction]
    public async Task<int> MapMenus(MapMenusReq req)
    {
        if (!await Repository.Select.AnyAsync(a => a.Id == req.RoleId)) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.The_character_id_does_not_exist);
        }

        if (await Repository.Orm.Select<TbSysMenu>().Where(a => req.MenuNames.Contains(a.Name)).CountAsync() !=
            req.MenuNames.Count) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.Contains_a_menu_that_does_not_exist);
        }

        //删除原有菜单映射
        await Repository.Orm.Delete<TbSysRoleMenu>().Where(a => a.RoleId == req.RoleId).ExecuteAffrowsAsync();

        //插入新的端点映射
        var inserts = req.MenuNames.Select(x => new TbSysRoleMenu { RoleId = req.RoleId, MenuName = x });
        var ret     = await Repository.Orm.Insert(inserts).ExecuteAffrowsAsync();
        return ret;
    }

    /// <summary>
    ///     分页查询角色
    /// </summary>
    public async Task<PagedQueryRsp<QueryRoleRsp>> PagedQuery(PagedQueryReq<QueryRoleReq> req)
    {
        var list = await Repository.Select.IncludeMany(a => a.Depts)
                                   .IncludeMany(a => a.Menus)
                                   .WhereDynamicFilter(req.DynamicFilter)
                                   .WhereDynamic(req.Filter)
                                   .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop
                                  ,                                             req.Order == Enums.Orders.Ascending)
                                   .OrderByIf(req.Prop is not { Length: > 0 }, a => a.Sort, true)
                                   .Page(req.Page, req.PageSize)
                                   .Count(out var total)
                                   .ToListAsync();

        return new PagedQueryRsp<QueryRoleRsp>(req.Page, req.PageSize, total
                                             , list.Select(x => x.Adapt<QueryRoleRsp>()));
    }

    /// <summary>
    ///     查询角色
    /// </summary>
    public async Task<List<QueryRoleRsp>> Query(QueryReq<QueryRoleReq> req)
    {
        var ret = await Repository.Select.IncludeMany(a => a.Depts)
                                  .IncludeMany(a => a.Menus)
                                  .WhereDynamicFilter(req.DynamicFilter)
                                  .WhereDynamic(req.Filter)
                                  .ToListAsync();
        return ret.ConvertAll(x => x.Adapt<QueryRoleRsp>());
    }

    /// <summary>
    ///     更新角色
    /// </summary>
    [Transaction]
    public async Task<QueryRoleRsp> Update(UpdateRoleReq req)
    {
        if (await Repository.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            throw Oops.Oh(Enums.ErrorCodes.Unknown);
        }

        if (req.DataScope == Enums.DataScopes.SpecificDept) {
            _ = await MapDataScopeWithDept(req.Id, req.DpetIdsInDataScope);
        }

        var ret = await Repository.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryRoleRsp>();
    }

    private async Task<int> MapDataScopeWithDept(long roleId, IReadOnlyCollection<long> deptIds)
    {
        if (!deptIds.NullOrEmpty() &&
            await Repository.Orm.Select<TbSysDept>().ForUpdate().Where(a => deptIds.Contains(a.Id)).CountAsync() !=
            deptIds.Count) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.Include_departments_that_do_not_exist);
        }

        //删除原有部门映射
        await Repository.Orm.Delete<TbSysRoleDept>().Where(a => a.RoleId == roleId).ExecuteAffrowsAsync();

        //插入新的部门映射
        var inserts = deptIds.Select(x => new TbSysRoleDept { RoleId = roleId, DeptId = x });
        var ret     = await Repository.Orm.Insert(inserts).ExecuteAffrowsAsync();
        return ret;
    }
}