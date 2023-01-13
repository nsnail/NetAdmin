using Furion.DynamicApiController;
using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Log;

namespace NetAdmin.Application.Service.Sys.Implements;

/// <inheritdoc cref="ILogService" />
public class LogService : ServiceBase<LogService>, ILogService, IDynamicApiController
{
    private readonly ILoginLogService     _loginLogService;
    private readonly IOperationLogService _operationLogService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="LogService" /> class.
    /// </summary>
    public LogService(IOperationLogService operationLogService, ILoginLogService loginLogService) //
    {
        _operationLogService = operationLogService;
        _loginLogService     = loginLogService;
    }

    /// <inheritdoc />
    public async Task<QueryLoginLogRsp> CreateLoginLog(CreateLoginLogReq req)
    {
        return await _loginLogService.Create(req);
    }

    /// <inheritdoc />
    public async Task<QueryOperationLogRsp> CreateOperationLog(CreateOperationLogReq req)
    {
        return await _operationLogService.Create(req);
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryLoginLogRsp>> PagedQueryLoginLog(PagedQueryReq<QueryLoginLogReq> req)
    {
        return await _loginLogService.PagedQuery(req);
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryOperationLogRsp>> PagedQueryOperationLog(
        PagedQueryReq<QueryOperationLogReq> req)
    {
        return await _operationLogService.PagedQuery(req);
    }
}