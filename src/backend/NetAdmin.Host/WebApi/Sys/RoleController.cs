using NetAdmin.Host.Aop;

namespace NetAdmin.Host.WebApi.Sys;

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
    public async Task<int> BulkDelete(BulkDelReq req)
    {
        return await Service.BulkDelete(req);
    }

    /// <summary>
    ///     创建角色
    /// </summary>
    [Transaction]
    public async ValueTask<QueryRoleRsp> Create(CreateRoleReq req)
    {
        return await Service.Create(req);
    }

    /// <summary>
    ///     删除角色
    /// </summary>
    [Transaction]
    public async ValueTask<int> Delete(DelReq req)
    {
        return await Service.Delete(req);
    }

    /// <summary>
    ///     分页查询角色
    /// </summary>
    public async ValueTask<PagedQueryRsp<QueryRoleRsp>> PagedQuery(PagedQueryReq<QueryRoleReq> req)
    {
        return await Service.PagedQuery(req);
    }

    /// <summary>
    ///     查询角色
    /// </summary>
    public async ValueTask<List<QueryRoleRsp>> Query(QueryReq<QueryRoleReq> req)
    {
        return await Service.Query(req);
    }

    /// <summary>
    ///     更新角色
    /// </summary>
    [Transaction]
    public async ValueTask<QueryRoleRsp> Update(UpdateRoleReq req)
    {
        return await Service.Update(req);
    }
}