using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Log;

namespace NetAdmin.Application.Services.Sys;

/// <summary>
///     日志服务
/// </summary>
public interface ILogService : IService
{
    /// <summary>
    ///     创建操作日志
    /// </summary>
    ValueTask<QueryLoginLogRsp> CreateLoginLog(CreateLoginLogReq req);

    /// <summary>
    ///     创建操作日志
    /// </summary>
    ValueTask<QueryOperationLogRsp> CreateOperationLog(CreateOperationLogReq req);

    /// <summary>
    ///     分页查询登录日志
    /// </summary>
    ValueTask<PagedQueryRsp<QueryLoginLogRsp>> PagedQueryLoginLog(PagedQueryReq<QueryLoginLogReq> req);

    /// <summary>
    ///     分页查询操作日志
    /// </summary>
    ValueTask<PagedQueryRsp<QueryOperationLogRsp>> PagedQueryOperationLog(PagedQueryReq<QueryOperationLogReq> req);
}