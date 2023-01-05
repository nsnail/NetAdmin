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
public class RoleApi : CrudApi<TbSysRole, IRoleApi>, IRoleApi
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RoleApi" /> class.
    /// </summary>
    public RoleApi(Repository<TbSysRole> repository) //
        : base(repository) { }

    /// <inheritdoc />
    public async Task Create(CreateRoleReq req)
    {
        await Repository.InsertAsync(req);
    }

    /// <inheritdoc />
    public async Task<int> Delete(DelReq req)
    {
        if (await Repository.Orm.Select<TbSysUserRole>().AnyAsync(a => a.RoleId == req.Id)) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, "该角色下存在用户，不允许删除");
        }

        var ret = await Repository.DeleteAsync(x => x.Id == req.Id);
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
        var inserts = req.Paths.ConvertAll(x => new TbSysRoleEndpoint { RoleId = req.RoleId, Path = x });
        var ret     = await Repository.Orm.Insert(inserts).ExecuteAffrowsAsync();
        return ret;
    }

    /// <inheritdoc />
    public async Task<List<RoleInfo>> Query()
    {
        var ret = await Repository.Where(x => true).ToListAsync();
        return ret.ConvertAll(x => x.Adapt<RoleInfo>());
    }

    /// <inheritdoc />
    public async Task<int> Update(UpdateRoleReq req)
    {
        var ret = await Repository.UpdateDiy.SetSource(req).ExecuteAffrowsAsync();
        return ret;
    }
}