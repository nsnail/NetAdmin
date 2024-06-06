using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.RequestLog;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     请求日志模块
/// </summary>
public interface IRequestLogModule : ICrudModule<CreateRequestLogReq, QueryRequestLogRsp // 创建类型
  , QueryRequestLogReq, QueryRequestLogRsp                                               // 查询类型
  , DelReq                                                                               // 删除类型
>
{
    /// <summary>
    ///     获取条形图数据
    /// </summary>
    Task<IOrderedEnumerable<GetBarChartRsp>> GetBarChartAsync(QueryReq<QueryRequestLogReq> req);

    /// <summary>
    ///     描述分组饼图数据
    /// </summary>
    Task<IOrderedEnumerable<GetPieChartRsp>> GetPieChartByApiSummaryAsync(QueryReq<QueryRequestLogReq> req);

    /// <summary>
    ///     状态码分组饼图数据
    /// </summary>
    Task<IOrderedEnumerable<GetPieChartRsp>> GetPieChartByHttpStatusCodeAsync(QueryReq<QueryRequestLogReq> req);
}