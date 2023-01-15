using NetAdmin.Application.Modules.Sys;

namespace NetAdmin.Application.Services.Sys.Dependency;

/// <summary>
///     日志服务
/// </summary>
public interface ILogService : IService, ILogModule
{
    /// <summary>
    ///     创建操作日志
    /// </summary>
    ValueTask<QueryLoginLogRsp> CreateLoginLog(CreateLoginLogReq req);

    /// <summary>
    ///     创建操作日志
    /// </summary>
    ValueTask<QueryOperationLogRsp> CreateOperationLog(CreateOperationLogReq req);
}