using Furion.FriendlyException;
using Mapster;
using NetAdmin.Aop.Attributes;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Role;
using NetAdmin.Infrastructure.Constant;
using NetAdmin.Repositories;

namespace NetAdmin.Api.Sys.Implements;

/// <inheritdoc cref="IRoleApi" />
public class RoleApi : RepositoryApi<TbSysRole, IRoleApi>, IRoleApi
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RoleApi" /> class.
    /// </summary>
    public RoleApi(Repository<TbSysRole> repository) //
        : base(repository) { }

    /// <summary>
    ///     创建角色
    /// </summary>
    public async Task<QueryRoleRsp> Create(CreateRoleReq req)
    {
        var ret = await Repository.InsertAsync(req);
        return ret.Adapt<QueryRoleRsp>();
    }

    /// <summary>
    ///     删除角色
    /// </summary>
    [Transaction]
    public async Task<int> Delete(DelReq req)
    {
        if (await Repository.Orm.Select<TbSysUserRole>().ForUpdate().AnyAsync(a => a.RoleId == req.Id)) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, "该角色下存在用户，不允许删除");
        }

        var ret = await Repository.DeleteAsync(a => a.Id == req.Id);
        return ret;
    }

    /// <inheritdoc />
    [Transaction]
    public async Task<int> MapEndpoints(MapEndpointsReq req)
    {
        if (!await Repository.Select.AnyAsync(a => a.Id == req.RoleId)) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, "角色id不存在");
        }

        if (await Repository.Orm.Select<TbSysEndpoint>().Where(a => req.Paths.Contains(a.Path)).CountAsync() !=
            req.Paths.Count) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, "包含了不存在的端点路径");
        }

        //删除原有端点映射
        await Repository.Orm.Delete<TbSysRoleEndpoint>().Where(a => a.RoleId == req.RoleId).ExecuteAffrowsAsync();

        //插入新的端点映射
        var inserts = req.Paths.Select(x => new TbSysRoleEndpoint { RoleId = req.RoleId, Path = x });
        var ret     = await Repository.Orm.Insert(inserts).ExecuteAffrowsAsync();
        return ret;
    }

    /// <summary>
    ///     分页查询角色
    /// </summary>
    public async Task<PagedQueryRsp<QueryRoleRsp>> PagedQuery(PagedQueryReq<QueryRoleReq> req)
    {
        var list = await Repository.Select.WhereDynamicFilter(req.DynamicFilter)
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
        var ret = await Repository.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter).ToListAsync();
        return ret.ConvertAll(x => x.Adapt<QueryRoleRsp>());
    }

    /// <summary>
    ///     更新角色
    /// </summary>
    public async Task<QueryRoleRsp> Update(UpdateRoleReq req)
    {
        if (await Repository.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            throw Oops.Oh(Enums.ErrorCodes.Unknown);
        }

        var ret = await Repository.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryRoleRsp>();
    }
}