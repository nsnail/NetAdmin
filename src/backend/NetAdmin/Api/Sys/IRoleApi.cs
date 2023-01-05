using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Role;

namespace NetAdmin.Api.Sys;

/// <summary>
///     角色接口
/// </summary>
public interface IRoleApi : ICrudApi<CreateRoleReq // 创建类型
                              , RoleInfo, RoleInfo // 查询类型
                              , UpdateRoleReq      // 修改类型
                              , DelReq             // 删除类型
                            >, IRestfulApi
{
    /// <summary>
    ///     角色端点映射
    /// </summary>
    Task<int> MapEndpoints(MapEndpointsReq req);
}