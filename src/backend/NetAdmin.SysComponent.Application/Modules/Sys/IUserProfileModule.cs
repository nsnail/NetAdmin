using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.UserProfile;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     用户档案模块
/// </summary>
public interface IUserProfileModule : ICrudModule<CreateUserProfileReq, QueryUserProfileRsp // 创建类型
  , QueryUserProfileReq, QueryUserProfileRsp                                                // 查询类型
  , UpdateUserProfileReq, QueryUserProfileRsp                                               // 修改类型
  , DelReq                                                                                  // 删除类型
>;