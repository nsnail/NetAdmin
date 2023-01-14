using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Role;

namespace NetAdmin.Application.Services.Sys;

/// <summary>
///     角色服务
/// </summary>
public interface IRoleService : ICrudService<CreateRoleReq, QueryRoleRsp // 创建类型
  , QueryRoleReq, QueryRoleRsp                                           // 查询类型
  , UpdateRoleReq, QueryRoleRsp                                          // 修改类型
  , DelReq                                                               // 删除类型
>
{
    /// <summary>
    ///     批量删除角色
    /// </summary>
    Task<int> BulkDelete(BulkDelReq req);
}