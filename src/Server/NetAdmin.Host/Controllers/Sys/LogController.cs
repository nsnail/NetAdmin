using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.RequestLog;
using NetAdmin.Host.Attributes;

namespace NetAdmin.Host.Controllers.Sys;

/// <summary>
///     请求日志服务
/// </summary>
public class LogController : ControllerBase<IRequestLogService>, IRequestLogModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LogController" /> class.
    /// </summary>
    public LogController(IRequestLogService service) //
        : base(service) { }

    /// <summary>
    ///     批量删除请求日志
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     创建请求日志
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<QueryRequestLogRsp> CreateAsync(CreateRequestLogReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <summary>
    ///     删除请求日志
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <summary>
    ///     分页查询请求日志
    /// </summary>
    public Task<PagedQueryRsp<QueryRequestLogRsp>> PagedQueryAsync(PagedQueryReq<QueryRequestLogReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询请求日志
    /// </summary>
    public Task<IEnumerable<QueryRequestLogRsp>> QueryAsync(QueryReq<QueryRequestLogReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <summary>
    ///     更新请求日志
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<NopReq> UpdateAsync(NopReq req)
    {
        return Service.UpdateAsync(req);
    }
}