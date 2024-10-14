using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.RequestLog;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     请求日志服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class RequestLogController(IRequestLogCache cache) : ControllerBase<IRequestLogCache, IRequestLogService>(cache), IRequestLogModule
{
    /// <summary>
    ///     批量删除请求日志
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     请求日志计数
    /// </summary>
    public Task<long> CountAsync(QueryReq<QueryRequestLogReq> req)
    {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     创建请求日志
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<QueryRequestLogRsp> CreateAsync(CreateRequestLogReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除请求日志
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     请求日志是否存在
    /// </summary>
    [NonAction]
    public Task<bool> ExistAsync(QueryReq<QueryRequestLogReq> req)
    {
        return Cache.ExistAsync(req);
    }

    /// <summary>
    ///     导出请求日志
    /// </summary>
    public Task<IActionResult> ExportAsync(QueryReq<QueryRequestLogReq> req)
    {
        return Cache.ExportAsync(req);
    }

    /// <summary>
    ///     获取单个请求日志
    /// </summary>
    public Task<QueryRequestLogRsp> GetAsync(QueryRequestLogReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     获取条形图数据
    /// </summary>
    public Task<IEnumerable<GetBarChartRsp>> GetBarChartAsync(QueryReq<QueryRequestLogReq> req)
    {
        return Cache.GetBarChartAsync(req);
    }

    /// <summary>
    ///     描述分组饼图数据
    /// </summary>
    public Task<IEnumerable<GetPieChartRsp>> GetPieChartByApiSummaryAsync(QueryReq<QueryRequestLogReq> req)
    {
        return Cache.GetPieChartByApiSummaryAsync(req);
    }

    /// <summary>
    ///     状态码分组饼图数据
    /// </summary>
    public Task<IEnumerable<GetPieChartRsp>> GetPieChartByHttpStatusCodeAsync(QueryReq<QueryRequestLogReq> req)
    {
        return Cache.GetPieChartByHttpStatusCodeAsync(req);
    }

    /// <summary>
    ///     分页查询请求日志
    /// </summary>
    public Task<PagedQueryRsp<QueryRequestLogRsp>> PagedQueryAsync(PagedQueryReq<QueryRequestLogReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询请求日志
    /// </summary>
    public Task<IEnumerable<QueryRequestLogRsp>> QueryAsync(QueryReq<QueryRequestLogReq> req)
    {
        return Cache.QueryAsync(req);
    }
}