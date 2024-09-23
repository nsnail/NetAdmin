using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Adm.GirlFriends;
using NetAdmin.Domain.Dto.Dependency;

namespace NetAdmin.AdmServer.Application.Modules.Adm;

/// <summary>
///     女朋友模块
/// </summary>
public interface IGirlFriendsModule : ICrudModule<CreateGirlFriendsReq, QueryGirlFriendsRsp // 创建类型
  , QueryGirlFriendsReq, QueryGirlFriendsRsp                                                // 查询类型
  , DelReq                                                                                  // 删除类型
>
{
    /// <summary>
    /// 编辑女朋友
    /// </summary>
    /// <param name="req">请求参数</param>
    /// <returns>女朋友信息</returns>
    Task<QueryGirlFriendsRsp> EditAsync(EditGirlFriendsReq req);
}