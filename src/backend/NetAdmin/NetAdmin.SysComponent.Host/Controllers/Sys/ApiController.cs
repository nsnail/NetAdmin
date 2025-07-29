using NetAdmin.Domain.Dto.Sys.Api;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     接口服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class ApiController(IApiCache cache) : ControllerBase<IApiCache, IApiService>(cache), IApiModule
{
    /// <summary>
    ///     批量删除接口
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     接口计数
    /// </summary>
    [NonAction]
    public Task<long> CountAsync(QueryReq<QueryApiReq> req)
    {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     接口分组计数
    /// </summary>
    [NonAction]
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryApiReq> req)
    {
        return Cache.CountByAsync(req);
    }

    /// <summary>
    ///     创建接口
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<QueryApiRsp> CreateAsync(CreateApiReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除接口
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     编辑接口
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<QueryApiRsp> EditAsync(EditApiReq req)
    {
        return Cache.EditAsync(req);
    }

    /// <summary>
    ///     导出接口
    /// </summary>
    public Task<IActionResult> ExportAsync(QueryReq<QueryApiReq> req)
    {
        return Cache.ExportAsync(req);
    }

    /// <summary>
    ///     获取单个接口
    /// </summary>
    [NonAction]
    public Task<QueryApiRsp> GetAsync(QueryApiReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     分页查询接口
    /// </summary>
    [NonAction]
    public Task<PagedQueryRsp<QueryApiRsp>> PagedQueryAsync(PagedQueryReq<QueryApiReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     平面查询接口
    /// </summary>
    public Task<IEnumerable<QueryApiRsp>> PlainQueryAsync(QueryReq<QueryApiReq> req)
    {
        return Cache.PlainQueryAsync(req);
    }

    /// <summary>
    ///     查询接口
    /// </summary>
    public Task<IEnumerable<QueryApiRsp>> QueryAsync(QueryReq<QueryApiReq> req)
    {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     接口求和
    /// </summary>
    [NonAction]
    public Task<decimal> SumAsync(QueryReq<QueryApiReq> req)
    {
        return Cache.SumAsync(req);
    }

    /// <summary>
    ///     同步接口
    /// </summary>
    [Transaction]
    public Task SyncAsync()
    {
        return Cache.SyncAsync();
    }
}