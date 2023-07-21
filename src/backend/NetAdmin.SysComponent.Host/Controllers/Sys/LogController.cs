using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.RequestLog;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     请求日志服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class LogController : ControllerBase<IRequestLogService>, IRequestLogModule
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
    ///     请求日志是否存在
    /// </summary>
    [NonAction]
    public Task<bool> ExistAsync(QueryReq<QueryRequestLogReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <summary>
    ///     获取单个请求日志
    /// </summary>
    [NonAction]
    public Task<QueryRequestLogRsp> GetAsync(QueryRequestLogReq req)
    {
        return Service.GetAsync(req);
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