using NetAdmin.Domain.Dto.Sys.UserInvite;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     用户邀请模块
/// </summary>
public interface IUserInviteModule : ICrudModule<CreateUserInviteReq, QueryUserInviteRsp // 创建类型
  , EditUserInviteReq                                                                    // 编辑类型
  , QueryUserInviteReq, QueryUserInviteRsp                                               // 查询类型
  , DelReq                                                                               // 删除类型
>
{
    /// <summary>
    ///     设置返佣比率
    /// </summary>
    Task<int> SetCommissionRatioAsync(SetCommissionRatioReq req);
}