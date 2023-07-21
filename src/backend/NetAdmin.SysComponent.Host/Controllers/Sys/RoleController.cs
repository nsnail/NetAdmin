using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Role;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     角色服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class RoleController : ControllerBase<IRoleService>, IRoleModule
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
    ///     角色是否存在
    /// </summary>
    [NonAction]
    public Task<bool> ExistAsync(QueryReq<QueryRoleReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <summary>
    ///     获取单个角色
    /// </summary>
    [NonAction]
    public Task<QueryRoleRsp> GetAsync(QueryRoleReq req)
    {
        return Service.GetAsync(req);
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