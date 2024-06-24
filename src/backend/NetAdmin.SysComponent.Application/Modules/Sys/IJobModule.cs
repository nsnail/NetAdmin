using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.Job;
using NetAdmin.Domain.Dto.Sys.JobRecord;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     计划作业模块
/// </summary>
public interface IJobModule : ICrudModule<CreateJobReq, QueryJobRsp // 创建类型
  , QueryJobReq, QueryJobRsp                                        // 查询类型
  , DelReq                                                          // 删除类型
>
{
    /// <summary>
    ///     编辑作业
    /// </summary>
    Task<QueryJobRsp> EditAsync(EditJobReq req);

    /// <summary>
    ///     执行作业
    /// </summary>
    Task ExecuteAsync(QueryJobReq req);

    /// <summary>
    ///     获取作业记录条形图数据
    /// </summary>
    Task<IOrderedEnumerable<GetBarChartRsp>> GetRecordBarChartAsync(QueryReq<QueryJobRecordReq> req);

    /// <summary>
    ///     状态码分组作业记录饼图数据
    /// </summary>
    Task<IOrderedEnumerable<GetPieChartRsp>> GetRecordPieChartByHttpStatusCodeAsync(QueryReq<QueryJobRecordReq> req);

    /// <summary>
    ///     名称分组作业记录饼图数据
    /// </summary>
    Task<IOrderedEnumerable<GetPieChartRsp>> GetRecordPieChartByNameAsync(QueryReq<QueryJobRecordReq> req);

    /// <summary>
    ///     获取单个作业记录
    /// </summary>
    Task<QueryJobRecordRsp> RecordGetAsync(QueryJobRecordReq req);

    /// <summary>
    ///     分页查询作业记录
    /// </summary>
    Task<PagedQueryRsp<QueryJobRecordRsp>> RecordPagedQueryAsync(PagedQueryReq<QueryJobRecordReq> req);

    /// <summary>
    ///     设置计划作业启用状态
    /// </summary>
    Task<int> SetEnabledAsync(SetJobEnabledReq req);
}