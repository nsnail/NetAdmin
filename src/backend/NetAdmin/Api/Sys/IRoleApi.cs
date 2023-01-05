using NetAdmin.DataContract.Dto.Sys.Role;

namespace NetAdmin.Api.Sys;

/// <summary>
///     角色接口
/// </summary>
public interface IRoleApi : IRestfulApi
{
    /// <summary>
    ///     角色端点映射
    /// </summary>
    Task<int> MapEndpoints(MapEndpointsReq req);
}