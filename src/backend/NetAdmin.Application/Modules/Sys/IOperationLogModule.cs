namespace NetAdmin.Application.Modules.Sys;

/// <summary>
///     操作日志模块
/// </summary>
public interface IOperationLogModule : ICrudModule<CreateOperationLogReq, QueryOperationLogRsp // 创建类型
  , QueryOperationLogReq, QueryOperationLogRsp                                                 // 查询类型
  , NopReq, NopReq                                                                             // 修改类型
  , DelReq                                                                                     // 删除类型
> { }