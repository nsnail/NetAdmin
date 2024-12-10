using NetAdmin.Domain.Dto.Sys.UserRole;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     用户-角色映射模块
/// </summary>
public interface IUserRoleModule : ICrudModule<CreateUserRoleReq, QueryUserRoleRsp // 创建类型
  , EditUserRoleReq                                                                // 编辑类型
  , QueryUserRoleReq, QueryUserRoleRsp                                             // 查询类型
  , DelReq                                                                         // 删除类型
>;