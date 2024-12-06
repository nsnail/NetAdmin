using NetAdmin.Domain.Dto.Sys.RequestLogDetail;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     请求日志明细模块
/// </summary>
public interface IRequestLogDetailModule : ICrudModule<CreateRequestLogDetailReq, QueryRequestLogDetailRsp // 创建类型
  , EditRequestLogDetailReq                                                                                // 编辑类型
  , QueryRequestLogDetailReq, QueryRequestLogDetailRsp                                                     // 查询类型
  , DelReq                                                                                                 // 删除类型
>;