using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.RequestLog;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     请求日志模块
/// </summary>
public interface IRequestLogModule : ICrudModule<CreateRequestLogReq, QueryRequestLogRsp // 创建类型
  , QueryRequestLogReq, QueryRequestLogRsp                                               // 查询类型
  , NopReq, NopReq                                                                       // 修改类型
  , DelReq                                                                               // 删除类型
> { }