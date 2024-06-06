using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Role;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     角色模块
/// </summary>
public interface IRoleModule : ICrudModule<CreateRoleReq, QueryRoleRsp // 创建类型
  , QueryRoleReq, QueryRoleRsp                                         // 查询类型
  , DelReq                                                             // 删除类型
>
{
    /// <summary>
    ///     编辑角色
    /// </summary>
    Task<QueryRoleRsp> EditAsync(EditRoleReq req);
}