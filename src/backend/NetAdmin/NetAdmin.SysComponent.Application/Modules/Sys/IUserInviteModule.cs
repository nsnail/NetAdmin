using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.Domain.Dto.Sys.User;
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
    ///     创建粉丝账号
    /// </summary>
    Task<QueryUserRsp> CreateFansAccountAsync(CreateFansAccountReq req);

    /// <summary>
    ///     获取自己是否允许自助充值
    /// </summary>
    Task<bool> GetSelfRechargeAllowedAsync();

    /// <summary>
    ///     查询可分配的角色
    /// </summary>
    Task<IEnumerable<QueryDicContentRsp>> QueryRolesAllowApplyAsync();

    /// <summary>
    ///     设置返佣比率
    /// </summary>
    Task<int> SetCommissionRatioAsync(SetCommissionRatioReq req);

    /// <summary>
    ///     修改粉丝角色
    /// </summary>
    Task<int> SetFansRoleAsync(SetFansRoleReq req);

    /// <summary>
    ///     设置上级
    /// </summary>
    Task<int> SetInviterAsync(SetInviterReq req);

    /// <summary>
    ///     设置允许自助充值
    /// </summary>
    Task<int> SetSelfRechargeAllowedAsync(SetSelfRechargeAllowedReq req);
}