using NetAdmin.Domain.Dto.Sys.SiteMsg;
using NetAdmin.Domain.Dto.Sys.SiteMsgFlag;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     站内信服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class SiteMsgController(ISiteMsgCache cache) : ControllerBase<ISiteMsgCache, ISiteMsgService>(cache), ISiteMsgModule
{
    /// <summary>
    ///     批量删除站内信
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     站内信计数
    /// </summary>
    public Task<long> CountAsync(QueryReq<QuerySiteMsgReq> req)
    {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     站内信分组计数
    /// </summary>
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QuerySiteMsgReq> req)
    {
        return Cache.CountByAsync(req);
    }

    /// <summary>
    ///     创建站内信
    /// </summary>
    [Transaction]
    public Task<QuerySiteMsgRsp> CreateAsync(CreateSiteMsgReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除站内信
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     编辑站内信
    /// </summary>
    [Transaction]
    public Task<QuerySiteMsgRsp> EditAsync(EditSiteMsgReq req)
    {
        return Cache.EditAsync(req);
    }

    /// <summary>
    ///     导出站内信
    /// </summary>
    public Task<IActionResult> ExportAsync(QueryReq<QuerySiteMsgReq> req)
    {
        return Cache.ExportAsync(req);
    }

    /// <summary>
    ///     获取单个站内信
    /// </summary>
    public Task<QuerySiteMsgRsp> GetAsync(QuerySiteMsgReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     获取单个我的站内信
    /// </summary>
    public Task<QuerySiteMsgRsp> GetMineAsync(QuerySiteMsgReq req)
    {
        return Cache.GetMineAsync(req);
    }

    /// <summary>
    ///     分页查询站内信
    /// </summary>
    public Task<PagedQueryRsp<QuerySiteMsgRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     分页查询我的站内信
    /// </summary>
    public Task<PagedQueryRsp<QuerySiteMsgRsp>> PagedQueryMineAsync(PagedQueryReq<QuerySiteMsgReq> req)
    {
        return Cache.PagedQueryMineAsync(req);
    }

    /// <summary>
    ///     查询站内信
    /// </summary>
    public Task<IEnumerable<QuerySiteMsgRsp>> QueryAsync(QueryReq<QuerySiteMsgReq> req)
    {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     设置站内信状态
    /// </summary>
    public Task SetSiteMsgStatusAsync(SetUserSiteMsgStatusReq req)
    {
        return Cache.SetSiteMsgStatusAsync(req);
    }

    /// <summary>
    ///     未读数量
    /// </summary>
    public Task<long> UnreadCountAsync()
    {
        return Cache.UnreadCountAsync();
    }
}