using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using YourSolution.AdmServer.Domain.Dto.Adm.GirlFriends;

namespace YourSolution.AdmServer.Application.Modules.Adm;

/// <summary>
///     女朋友模块
/// </summary>
public interface IGirlFriendsModule : ICrudModule<CreateGirlFriendsReq, QueryGirlFriendsRsp // 创建类型
  , EditGirlFriendsReq                                                              // 编辑类型
  , QueryGirlFriendsReq, QueryGirlFriendsRsp                                            // 查询类型
  , DelReq                                                                      // 删除类型
>;