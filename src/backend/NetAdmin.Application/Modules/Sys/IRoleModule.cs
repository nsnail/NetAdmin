using NetAdmin.DataContract.Dto.Dependency;
using NetAdmin.DataContract.Dto.Sys.Role;

namespace NetAdmin.Application.Modules.Sys;

/// <summary>
///     角色模块
/// </summary>
public interface IRoleModule : ICrudModule<CreateRoleReq, QueryRoleRsp // 创建类型
  , QueryRoleReq, QueryRoleRsp                                         // 查询类型
  , UpdateRoleReq, QueryRoleRsp                                        // 修改类型
  , DelReq                                                             // 删除类型
> { }