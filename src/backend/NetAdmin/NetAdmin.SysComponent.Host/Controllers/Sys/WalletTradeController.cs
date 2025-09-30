using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.WalletTrade;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     钱包交易服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class WalletTradeController(IWalletTradeCache cache) : ControllerBase<IWalletTradeCache, IWalletTradeService>(cache), IWalletTradeModule
{
    /// <summary>
    ///     批量删除钱包交易
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req) {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     钱包交易计数
    /// </summary>
    [NonAction]
    public Task<long> CountAsync(QueryReq<QueryWalletTradeReq> req) {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     钱包交易分组计数
    /// </summary>
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryWalletTradeReq> req) {
        return Cache.CountByAsync(req);
    }

    /// <summary>
    ///     创建钱包交易
    /// </summary>
    [Transaction]
    public Task<QueryWalletTradeRsp> CreateAsync(CreateWalletTradeReq req) {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除钱包交易
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req) {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     编辑钱包交易
    /// </summary>
    [Transaction]
    public Task<QueryWalletTradeRsp> EditAsync(EditWalletTradeReq req) {
        return Cache.EditAsync(req);
    }

    /// <summary>
    ///     导出钱包交易
    /// </summary>
    public Task<IActionResult> ExportAsync(QueryReq<QueryWalletTradeReq> req) {
        return Cache.ExportAsync(req);
    }

    /// <summary>
    ///     获取单个钱包交易
    /// </summary>
    public Task<QueryWalletTradeRsp> GetAsync(QueryWalletTradeReq req) {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     获取自助充值条形图数据
    /// </summary>
    public Task<IEnumerable<GetBarChartRsp>> GetSelfDepositBarChartAsync(QueryReq<QueryWalletTradeReq> req) {
        return Cache.GetSelfDepositBarChartAsync(req);
    }

    /// <summary>
    ///     分页查询钱包交易
    /// </summary>
    public Task<PagedQueryRsp<QueryWalletTradeRsp>> PagedQueryAsync(PagedQueryReq<QueryWalletTradeReq> req) {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询钱包交易
    /// </summary>
    [NonAction]
    public Task<IEnumerable<QueryWalletTradeRsp>> QueryAsync(QueryReq<QueryWalletTradeReq> req) {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     钱包交易求和
    /// </summary>
    public Task<decimal> SumAsync(QueryReq<QueryWalletTradeReq> req) {
        return Cache.SumAsync(req);
    }

    /// <summary>
    ///     消费总数计数
    /// </summary>
    public Task<long> TotalAmountAsync(QueryReq<QueryWalletTradeReq> req) {
        return Cache.TotalAmountAsync(req);
    }

    /// <summary>
    ///     从他人账户转账给自己
    /// </summary>
    [Transaction]
    public Task<int> TransferFromAnotherAccountAsync(TransferReq req) {
        return Cache.TransferFromAnotherAccountAsync(req);
    }

    /// <summary>
    ///     转账到他人账户
    /// </summary>
    [Transaction]
    public Task<int> TransferToAnotherAccountAsync(TransferReq req) {
        return Cache.TransferToAnotherAccountAsync(req);
    }
}