using NetAdmin.Domain.Dto.Sys.Role;
using NetAdmin.Domain.Dto.Sys.UserRole;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     角色模块
/// </summary>
public interface IRoleModule : ICrudModule<CreateRoleReq, QueryRoleRsp // 创建类型
    , EditRoleReq // 编辑类型
    , QueryRoleReq, QueryRoleRsp // 查询类型
    , DelReq // 删除类型
>
{
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

    /// <summary>
    ///     角色用户映射分组计数
    /// </summary>
    Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> UserCountByAsync(QueryReq<QueryUserRoleReq> req);
}