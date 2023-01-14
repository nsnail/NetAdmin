using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Log;

namespace NetAdmin.Application.Services.Sys;

/// <summary>
///     登录日志服务
/// </summary>
public interface ILoginLogService : ICrudService<CreateLoginLogReq, QueryLoginLogRsp // 创建类型
  , QueryLoginLogReq, QueryLoginLogRsp                                               // 查询类型
  , NopReq, NopReq                                                                   // 修改类型
  , DelReq                                                                           // 删除类型
> { }