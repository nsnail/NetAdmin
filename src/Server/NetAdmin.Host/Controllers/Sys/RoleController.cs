using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Role;
using NetAdmin.Host.Attributes;

namespace NetAdmin.Host.Controllers.Sys;

/// <summary>
///     角色服务
/// </summary>
public class RoleController : ControllerBase<IRoleService>, IRoleModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RoleController" /> class.
    /// </summary>
    public RoleController(IRoleService service) //
        : base(service) { }

    /// <summary>
    ///     批量删除角色
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     创建角色
    /// </summary>
    [Transaction]
    public Task<QueryRoleRsp> CreateAsync(CreateRoleReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <summary>
    ///     删除角色
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <summary>
    ///     分页查询角色
    /// </summary>
    public Task<PagedQueryRsp<QueryRoleRsp>> PagedQueryAsync(PagedQueryReq<QueryRoleReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询角色
    /// </summary>
    public Task<IEnumerable<QueryRoleRsp>> QueryAsync(QueryReq<QueryRoleReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <summary>
    ///     更新角色
    /// </summary>
    [Transaction]
    public Task<QueryRoleRsp> UpdateAsync(UpdateRoleReq req)
    {
        return Service.UpdateAsync(req);
    }
}