using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Log;

namespace NetAdmin.Application.Service.Sys;

/// <summary>
///     日志服务
/// </summary>
public interface ILogService
{
    /// <summary>
    ///     创建操作日志
    /// </summary>
    public Task<QueryLoginLogRsp> CreateLoginLog(CreateLoginLogReq req);

    /// <summary>
    ///     创建操作日志
    /// </summary>
    public Task<QueryOperationLogRsp> CreateOperationLog(CreateOperationLogReq req);

    /// <summary>
    ///     分页查询登录日志
    /// </summary>
    public Task<PagedQueryRsp<QueryLoginLogRsp>> PagedQueryLoginLog(PagedQueryReq<QueryLoginLogReq> req);

    /// <summary>
    ///     分页查询操作日志
    /// </summary>
    public Task<PagedQueryRsp<QueryOperationLogRsp>> PagedQueryOperationLog(PagedQueryReq<QueryOperationLogReq> req);
}