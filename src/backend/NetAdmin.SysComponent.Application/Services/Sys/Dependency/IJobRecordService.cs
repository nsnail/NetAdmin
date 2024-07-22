using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.JobRecord;
using NetAdmin.SysComponent.Application.Modules.Sys;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     计划作业执行记录服务
/// </summary>
public interface IJobRecordService : IService, IJobRecordModule
{
    /// <summary>
    ///     获取条形图数据
    /// </summary>
    Task<IEnumerable<GetBarChartRsp>> GetBarChartAsync(QueryReq<QueryJobRecordReq> req);

    /// <summary>
    ///     状态码分组饼图数据
    /// </summary>
    Task<IEnumerable<GetPieChartRsp>> GetPieChartByHttpStatusCodeAsync(QueryReq<QueryJobRecordReq> req);

    /// <summary>
    ///     名称分组饼图数据
    /// </summary>
    Task<IEnumerable<GetPieChartRsp>> GetPieChartByNameAsync(QueryReq<QueryJobRecordReq> req);
}