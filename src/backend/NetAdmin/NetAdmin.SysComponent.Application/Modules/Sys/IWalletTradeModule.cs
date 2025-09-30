using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.WalletTrade;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     钱包交易模块
/// </summary>
public interface IWalletTradeModule : ICrudModule<CreateWalletTradeReq, QueryWalletTradeRsp // 创建类型
    , EditWalletTradeReq // 编辑类型
    , QueryWalletTradeReq, QueryWalletTradeRsp // 查询类型
    , DelReq // 删除类型
>
{
    /// <summary>
    ///     获取自助充值条形图数据
    /// </summary>
    Task<IEnumerable<GetBarChartRsp>> GetSelfDepositBarChartAsync(QueryReq<QueryWalletTradeReq> req);

    /// <summary>
    ///     消费总数计数
    /// </summary>
    Task<long> TotalAmountAsync(QueryReq<QueryWalletTradeReq> req);

    /// <summary>
    ///     从他人账户转账给自己
    /// </summary>
    Task<int> TransferFromAnotherAccountAsync(TransferReq req);

    /// <summary>
    ///     转账到他人账户
    /// </summary>
    Task<int> TransferToAnotherAccountAsync(TransferReq req);
}