using Microsoft.AspNetCore.Mvc;
using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.DataContract.Dto.Sys.RequestLog;

namespace NetAdmin.Host.WebApi.Sys;

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
    ///     创建请求日志
    /// </summary>
    [NonAction]
    public ValueTask<QueryRequestLogRsp> Create(CreateRequestLogReq req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     删除请求日志
    /// </summary>
    [NonAction]
    public ValueTask<int> Delete(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     分页查询请求日志
    /// </summary>
    public async ValueTask<PagedQueryRsp<QueryRequestLogRsp>> PagedQuery(PagedQueryReq<QueryRequestLogReq> req)
    {
        return await Service.PagedQuery(req);
    }

    /// <summary>
    ///     查询请求日志
    /// </summary>
    public async ValueTask<List<QueryRequestLogRsp>> Query(QueryReq<QueryRequestLogReq> req)
    {
        return await Service.Query(req);
    }

    /// <summary>
    ///     更新请求日志
    /// </summary>
    [NonAction]
    public ValueTask<NopReq> Update(NopReq req)
    {
        throw new NotImplementedException();
    }
}