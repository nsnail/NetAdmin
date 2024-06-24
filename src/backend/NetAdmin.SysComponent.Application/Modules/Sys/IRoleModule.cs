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

    /// <summary>
    ///     设置是否显示仪表板
    /// </summary>
    Task<int> SetDisplayDashboardAsync(SetDisplayDashboardReq req);

    /// <summary>
    ///     启用/禁用角色
    /// </summary>
    Task<int> SetEnabledAsync(SetRoleEnabledReq req);

    /// <summary>
    ///     设置是否忽略权限控制
    /// </summary>
    Task<int> SetIgnorePermissionControlAsync(SetIgnorePermissionControlReq req);
}