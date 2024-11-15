using NetAdmin.Domain.Dto.Sys.LoginLog;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     登录日志服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class LoginLogController(ILoginLogCache cache) : ControllerBase<ILoginLogCache, ILoginLogService>(cache), ILoginLogModule
{
    /// <summary>
    ///     批量删除登录日志
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     登录日志计数
    /// </summary>
    public Task<long> CountAsync(QueryReq<QueryLoginLogReq> req)
    {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     创建登录日志
    /// </summary>
    [Transaction]
    public Task<QueryLoginLogRsp> CreateAsync(CreateLoginLogReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除登录日志
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     登录日志是否存在
    /// </summary>
    public Task<bool> ExistAsync(QueryReq<QueryLoginLogReq> req)
    {
        return Cache.ExistAsync(req);
    }

    /// <summary>
    ///     导出登录日志
    /// </summary>
    public Task<IActionResult> ExportAsync(QueryReq<QueryLoginLogReq> req)
    {
        return Cache.ExportAsync(req);
    }

    /// <summary>
    ///     获取单个登录日志
    /// </summary>
    public Task<QueryLoginLogRsp> GetAsync(QueryLoginLogReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     分页查询登录日志
    /// </summary>
    public Task<PagedQueryRsp<QueryLoginLogRsp>> PagedQueryAsync(PagedQueryReq<QueryLoginLogReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询登录日志
    /// </summary>
    public Task<IEnumerable<QueryLoginLogRsp>> QueryAsync(QueryReq<QueryLoginLogReq> req)
    {
        return Cache.QueryAsync(req);
    }
}