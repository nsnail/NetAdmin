using NetAdmin.SysComponent.Domain.Dto.Sys.SiteMsgRole;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     站内信-角色映射模块
/// </summary>
public interface ISiteMsgRoleModule : ICrudModule<CreateSiteMsgRoleReq, QuerySiteMsgRoleRsp // 创建类型
  , QuerySiteMsgRoleReq, QuerySiteMsgRoleRsp                                                // 查询类型
  , DelReq                                                                                  // 删除类型
>;