using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.SiteMsg;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     站内信模块
/// </summary>
public interface ISiteMsgModule : ICrudModule<CreateSiteMsgReq, QuerySiteMsgRsp // 创建类型
  , QuerySiteMsgReq, QuerySiteMsgRsp                                            // 查询类型
  , UpdateSiteMsgReq, QuerySiteMsgRsp                                           // 修改类型
  , DelReq                                                                      // 删除类型
>
{
    /// <summary>
    ///     未读数量
    /// </summary>
    Task<long> UnreadCountAsync();
}