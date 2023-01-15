namespace NetAdmin.Application.Modules.Sys;

/// <summary>
///     日志模块
/// </summary>
public interface ILogModule
{
    /// <summary>
    ///     分页查询登录日志
    /// </summary>
    ValueTask<PagedQueryRsp<QueryLoginLogRsp>> PagedQueryLoginLog(PagedQueryReq<QueryLoginLogReq> req);

    /// <summary>
    ///     分页查询操作日志
    /// </summary>
    ValueTask<PagedQueryRsp<QueryOperationLogRsp>> PagedQueryOperationLog(PagedQueryReq<QueryOperationLogReq> req);

    /// <summary>
    ///     查询登录日志
    /// </summary>
    ValueTask<List<QueryLoginLogRsp>> QueryLoginLog(QueryReq<QueryLoginLogReq> req);

    /// <summary>
    ///     查询操作日志
    /// </summary>
    ValueTask<List<QueryOperationLogRsp>> QueryOperationLog(QueryReq<QueryOperationLogReq> req);
}