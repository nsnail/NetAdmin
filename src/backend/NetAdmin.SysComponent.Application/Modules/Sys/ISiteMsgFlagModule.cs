using NetAdmin.SysComponent.Domain.Dto.Sys.SiteMsgFlag;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     站内信标记模块
/// </summary>
public interface ISiteMsgFlagModule : ICrudModule<CreateSiteMsgFlagReq, QuerySiteMsgFlagRsp // 创建类型
  , QuerySiteMsgFlagReq, QuerySiteMsgFlagRsp                                                // 查询类型
  , DelReq                                                                                  // 删除类型
>;