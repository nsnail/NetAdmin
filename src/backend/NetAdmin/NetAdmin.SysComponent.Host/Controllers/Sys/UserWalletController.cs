using NetAdmin.Domain.Dto.Sys.UserWallet;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     用户钱包服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class UserWalletController(IUserWalletCache cache) : ControllerBase<IUserWalletCache, IUserWalletService>(cache), IUserWalletModule
{
    /// <summary>
    ///     批量删除用户钱包
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     用户钱包计数
    /// </summary>
    [NonAction]
    public Task<long> CountAsync(QueryReq<QueryUserWalletReq> req)
    {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     用户钱包分组计数
    /// </summary>
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryUserWalletReq> req)
    {
        return Cache.CountByAsync(req);
    }

    /// <summary>
    ///     创建用户钱包
    /// </summary>
    [Transaction]
    public Task<QueryUserWalletRsp> CreateAsync(CreateUserWalletReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除用户钱包
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     编辑用户钱包
    /// </summary>
    [Transaction]
    public Task<QueryUserWalletRsp> EditAsync(EditUserWalletReq req)
    {
        return Cache.EditAsync(req);
    }

    /// <summary>
    ///     导出用户钱包
    /// </summary>
    [NonAction]
    public Task<IActionResult> ExportAsync(QueryReq<QueryUserWalletReq> req)
    {
        return Cache.ExportAsync(req);
    }

    /// <summary>
    ///     获取单个用户钱包
    /// </summary>
    public Task<QueryUserWalletRsp> GetAsync(QueryUserWalletReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     分页查询用户钱包
    /// </summary>
    public Task<PagedQueryRsp<QueryUserWalletRsp>> PagedQueryAsync(PagedQueryReq<QueryUserWalletReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询用户钱包
    /// </summary>
    [NonAction]
    public Task<IEnumerable<QueryUserWalletRsp>> QueryAsync(QueryReq<QueryUserWalletReq> req)
    {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     用户钱包求和
    /// </summary>
    [NonAction]
    public Task<decimal> SumAsync(QueryReq<QueryUserWalletReq> req)
    {
        return Cache.SumAsync(req);
    }
}