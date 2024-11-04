using NetAdmin.SysComponent.Domain.Dto.Sys.Role;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     角色服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class RoleController(IRoleCache cache) : ControllerBase<IRoleCache, IRoleService>(cache), IRoleModule
{
    /// <summary>
    ///     批量删除角色
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     角色计数
    /// </summary>
    public Task<long> CountAsync(QueryReq<QueryRoleReq> req)
    {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     创建角色
    /// </summary>
    [Transaction]
    public Task<QueryRoleRsp> CreateAsync(CreateRoleReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除角色
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     编辑角色
    /// </summary>
    [Transaction]
    public Task<QueryRoleRsp> EditAsync(EditRoleReq req)
    {
        return Cache.EditAsync(req);
    }

    /// <summary>
    ///     角色是否存在
    /// </summary>
    [NonAction]
    public Task<bool> ExistAsync(QueryReq<QueryRoleReq> req)
    {
        return Cache.ExistAsync(req);
    }

    /// <summary>
    ///     导出角色
    /// </summary>
    public Task<IActionResult> ExportAsync(QueryReq<QueryRoleReq> req)
    {
        return Cache.ExportAsync(req);
    }

    /// <summary>
    ///     获取单个角色
    /// </summary>
    public Task<QueryRoleRsp> GetAsync(QueryRoleReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     分页查询角色
    /// </summary>
    public Task<PagedQueryRsp<QueryRoleRsp>> PagedQueryAsync(PagedQueryReq<QueryRoleReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询角色
    /// </summary>
    public Task<IEnumerable<QueryRoleRsp>> QueryAsync(QueryReq<QueryRoleReq> req)
    {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     设置是否显示仪表板
    /// </summary>
    public Task<int> SetDisplayDashboardAsync(SetDisplayDashboardReq req)
    {
        return Cache.SetDisplayDashboardAsync(req);
    }

    /// <summary>
    ///     启用/禁用角色
    /// </summary>
    public Task<int> SetEnabledAsync(SetRoleEnabledReq req)
    {
        return Cache.SetEnabledAsync(req);
    }

    /// <summary>
    ///     设置是否忽略权限控制
    /// </summary>
    public Task<int> SetIgnorePermissionControlAsync(SetIgnorePermissionControlReq req)
    {
        return Cache.SetIgnorePermissionControlAsync(req);
    }
}