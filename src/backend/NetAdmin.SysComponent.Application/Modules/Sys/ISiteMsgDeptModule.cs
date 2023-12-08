using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.SiteMsgDept;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     站内信-部门映射模块
/// </summary>
public interface ISiteMsgDeptModule : ICrudModule<CreateSiteMsgDeptReq, QuerySiteMsgDeptRsp // 创建类型
  , QuerySiteMsgDeptReq, QuerySiteMsgDeptRsp                                                // 查询类型
  , UpdateSiteMsgDeptReq, QuerySiteMsgDeptRsp                                               // 修改类型
  , DelReq                                                                                  // 删除类型
>;