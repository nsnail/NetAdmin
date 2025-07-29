using NetAdmin.Domain.Dto.Sys.WalletFrozen;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     钱包冻结服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class WalletFrozenController(IWalletFrozenCache cache)
    : ControllerBase<IWalletFrozenCache, IWalletFrozenService>(cache), IWalletFrozenModule
{
    /// <summary>
    ///     批量删除钱包冻结
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     钱包冻结计数
    /// </summary>
    [NonAction]
    public Task<long> CountAsync(QueryReq<QueryWalletFrozenReq> req)
    {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     钱包冻结分组计数
    /// </summary>
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryWalletFrozenReq> req)
    {
        return Cache.CountByAsync(req);
    }

    /// <summary>
    ///     创建钱包冻结
    /// </summary>
    [Transaction]
    public Task<QueryWalletFrozenRsp> CreateAsync(CreateWalletFrozenReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除钱包冻结
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     编辑钱包冻结
    /// </summary>
    [Transaction]
    public Task<QueryWalletFrozenRsp> EditAsync(EditWalletFrozenReq req)
    {
        return Cache.EditAsync(req);
    }

    /// <summary>
    ///     导出钱包冻结
    /// </summary>
    [NonAction]
    public Task<IActionResult> ExportAsync(QueryReq<QueryWalletFrozenReq> req)
    {
        return Cache.ExportAsync(req);
    }

    /// <summary>
    ///     获取单个钱包冻结
    /// </summary>
    public Task<QueryWalletFrozenRsp> GetAsync(QueryWalletFrozenReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     分页查询钱包冻结
    /// </summary>
    public Task<PagedQueryRsp<QueryWalletFrozenRsp>> PagedQueryAsync(PagedQueryReq<QueryWalletFrozenReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询钱包冻结
    /// </summary>
    [NonAction]
    public Task<IEnumerable<QueryWalletFrozenRsp>> QueryAsync(QueryReq<QueryWalletFrozenReq> req)
    {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     将状态设置为解冻
    /// </summary>
    [Transaction]
    public Task<int> SetStatusToThawedAsync(SetStatusToThawedReq req)
    {
        return Cache.SetStatusToThawedAsync(req);
    }

    /// <summary>
    ///     钱包冻结求和
    /// </summary>
    [NonAction]
    public Task<decimal> SumAsync(QueryReq<QueryWalletFrozenReq> req)
    {
        return Cache.SumAsync(req);
    }
}