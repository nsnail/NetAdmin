using NetAdmin.Domain.Dto.Sys.Role;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IRoleCache" />
public sealed class RoleCache(IDistributedCache cache, IRoleService service) //
    : DistributedCache<IRoleService>(cache, service), IScoped, IRoleCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryRoleReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryRoleRsp> CreateAsync(CreateRoleReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryRoleRsp> EditAsync(EditRoleReq req)
    {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryRoleReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryRoleRsp> GetAsync(QueryRoleReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryRoleRsp>> PagedQueryAsync(PagedQueryReq<QueryRoleReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryRoleRsp>> QueryAsync(QueryReq<QueryRoleReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<int> SetDisplayDashboardAsync(SetDisplayDashboardReq req)
    {
        return Service.SetDisplayDashboardAsync(req);
    }

    /// <inheritdoc />
    public Task<int> SetEnabledAsync(SetRoleEnabledReq req)
    {
        return Service.SetEnabledAsync(req);
    }

    /// <inheritdoc />
    public Task<int> SetIgnorePermissionControlAsync(SetIgnorePermissionControlReq req)
    {
        return Service.SetIgnorePermissionControlAsync(req);
    }
}