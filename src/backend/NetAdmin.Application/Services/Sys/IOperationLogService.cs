using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Log;

namespace NetAdmin.Application.Services.Sys;

/// <summary>
///     操作日志服务
/// </summary>
public interface IOperationLogService : ICrudService<CreateOperationLogReq, QueryOperationLogRsp // 创建类型
  , QueryOperationLogReq, QueryOperationLogRsp                                                   // 查询类型
  , NopReq, NopReq                                                                               // 修改类型
  , DelReq                                                                                       // 删除类型
> { }