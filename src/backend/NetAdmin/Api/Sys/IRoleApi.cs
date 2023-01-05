using NetAdmin.Api.Pub;
using NetAdmin.DataContract.Dto.Sys.Role;

namespace NetAdmin.Api.Sys;

/// <summary>
///     角色接口
/// </summary>
public interface IRoleApi : IApi
{
    /// <summary>
    ///     增加角色
    /// </summary>
    Task Create(CreateRoleReq req);

    /// <summary>
    ///     删除角色
    /// </summary>
    Task<int> Delete(DelReq req);

    /// <summary>
    ///     角色端点映射
    /// </summary>
    Task<int> MapEndpoints(MapEndpointsReq req);

    /// <summary>
    ///     查询角色
    /// </summary>
    Task<List<RoleInfo>> Query();

    /// <summary>
    ///     修改角色
    /// </summary>
    Task<int> Update(UpdateRoleReq req);
}