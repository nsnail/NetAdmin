using Microsoft.AspNetCore.Mvc;
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
    public async Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        return await Service.BulkDelete(req);
    }

    /// <summary>
    ///     创建请求日志
    /// </summary>
    [NonAction]
    [Transaction]
    public async Task<QueryRequestLogRsp> Create(CreateRequestLogReq req)
    {
        return await Service.Create(req);
    }

    /// <summary>
    ///     删除请求日志
    /// </summary>
    [NonAction]
    [Transaction]
    public async Task<int> Delete(DelReq req)
    {
        return await Service.Delete(req);
    }

    /// <summary>
    ///     分页查询请求日志
    /// </summary>
    public async Task<PagedQueryRsp<QueryRequestLogRsp>> PagedQuery(PagedQueryReq<QueryRequestLogReq> req)
    {
        return await Service.PagedQuery(req);
    }

    /// <summary>
    ///     查询请求日志
    /// </summary>
    public async Task<IEnumerable<QueryRequestLogRsp>> Query(QueryReq<QueryRequestLogReq> req)
    {
        return await Service.Query(req);
    }

    /// <summary>
    ///     更新请求日志
    /// </summary>
    [NonAction]
    [Transaction]
    public async Task<NopReq> Update(NopReq req)
    {
        return await Service.Update(req);
    }
}