using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.LoginLog;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     登录日志模块
/// </summary>
public interface ILoginLogModule : ICrudModule<CreateLoginLogReq, QueryLoginLogRsp // 创建类型
  , QueryLoginLogReq, QueryLoginLogRsp                                             // 查询类型
  , DelReq                                                                         // 删除类型
>;