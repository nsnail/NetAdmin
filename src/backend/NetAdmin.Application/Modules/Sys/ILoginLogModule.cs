namespace NetAdmin.Application.Modules.Sys;

/// <summary>
///     登录日志模块
/// </summary>
public interface ILoginLogModule : ICrudModule<CreateLoginLogReq, QueryLoginLogRsp // 创建类型
  , QueryLoginLogReq, QueryLoginLogRsp                                             // 查询类型
  , NopReq, NopReq                                                                 // 修改类型
  , DelReq                                                                         // 删除类型
> { }