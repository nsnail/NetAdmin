using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.SiteMsg;
using NetAdmin.Domain.Dto.Sys.SiteMsgFlag;

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
    ///     获取单个我的站内信
    /// </summary>
    Task<QuerySiteMsgRsp> GetMineAsync(QuerySiteMsgReq req);

    /// <summary>
    ///     分页查询我的站内信
    /// </summary>
    Task<PagedQueryRsp<QuerySiteMsgRsp>> PagedQueryMineAsync(PagedQueryReq<QuerySiteMsgReq> req);

    /// <summary>
    ///     设置站内信状态
    /// </summary>
    Task SetSiteMsgStatusAsync(UpdateSiteMsgFlagReq req);

    /// <summary>
    ///     未读数量
    /// </summary>
    Task<long> UnreadCountAsync();
}