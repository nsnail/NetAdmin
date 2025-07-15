using NetAdmin.Domain.Dto.Sys.WalletFrozen;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     钱包冻结模块
/// </summary>
public interface IWalletFrozenModule : ICrudModule<CreateWalletFrozenReq, QueryWalletFrozenRsp // 创建类型
  , EditWalletFrozenReq                                                                        // 编辑类型
  , QueryWalletFrozenReq, QueryWalletFrozenRsp                                                 // 查询类型
  , DelReq                                                                                     // 删除类型
>
{
    /// <summary>
    ///     将状态设置为解冻
    /// </summary>
    Task<int> SetStatusToThawedAsync(SetStatusToThawedReq req);
}