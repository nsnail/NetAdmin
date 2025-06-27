using NetAdmin.Domain.Dto.Sys.DepositOrder;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     充值订单模块
/// </summary>
public interface IDepositOrderModule : ICrudModule<CreateDepositOrderReq, QueryDepositOrderRsp // 创建类型
  , EditDepositOrderReq                                                                        // 编辑类型
  , QueryDepositOrderReq, QueryDepositOrderRsp                                                 // 查询类型
  , DelReq                                                                                     // 删除类型
>
{
    /// <summary>
    ///     获取充值配置
    /// </summary>
    Task<GetDepositConfigRsp> GetDepositConfigAsync();

    /// <summary>
    ///     支付确认
    /// </summary>
    Task<int> PayConfirmAsync(PayConfirmReq req);

    /// <summary>
    ///     到账确认
    /// </summary>
    Task<int> ReceivedConfirmationAsync(ReceivedConfirmationReq req);
}