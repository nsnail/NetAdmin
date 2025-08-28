using NetAdmin.Domain.Dto.Sys.SiteMsgUser;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     站内信-用户映射模块
/// </summary>
public interface ISiteMsgUserModule : ICrudModule<CreateSiteMsgUserReq, QuerySiteMsgUserRsp // 创建类型
    , EditSiteMsgUserReq // 编辑类型
    , QuerySiteMsgUserReq, QuerySiteMsgUserRsp // 查询类型
    , DelReq // 删除类型
>;