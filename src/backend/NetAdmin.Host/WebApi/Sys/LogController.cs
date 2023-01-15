using NetAdmin.Host.Aop;

namespace NetAdmin.Host.WebApi.Sys;

/// <summary>
///     日志服务
/// </summary>
public class LogController : ControllerBase<ILogService>, ILogModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LogController" /> class.
    /// </summary>
    public LogController(ILogService service) //
        : base(service) { }

    /// <summary>
    ///     分页查询登录日志
    /// </summary>
    public async ValueTask<PagedQueryRsp<QueryLoginLogRsp>> PagedQueryLoginLog(PagedQueryReq<QueryLoginLogReq> req)
    {
        return await Service.PagedQueryLoginLog(req);
    }

    /// <summary>
    ///     分页查询操作日志
    /// </summary>
    [JsonIgnoreNull]
    public async ValueTask<PagedQueryRsp<QueryOperationLogRsp>> PagedQueryOperationLog(
        PagedQueryReq<QueryOperationLogReq> req)
    {
        return await Service.PagedQueryOperationLog(req);
    }

    /// <summary>
    ///     查询登录日志
    /// </summary>
    public async ValueTask<List<QueryLoginLogRsp>> QueryLoginLog(QueryReq<QueryLoginLogReq> req)
    {
        return await Service.QueryLoginLog(req);
    }

    /// <summary>
    ///     查询操作日志
    /// </summary>
    public async ValueTask<List<QueryOperationLogRsp>> QueryOperationLog(QueryReq<QueryOperationLogReq> req)
    {
        return await Service.QueryOperationLog(req);
    }
}