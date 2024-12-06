using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.Job;
using NetAdmin.Domain.Dto.Sys.JobRecord;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     计划作业模块
/// </summary>
public interface IJobModule : ICrudModule<CreateJobReq, QueryJobRsp // 创建类型
  , EditJobReq                                                      // 编辑类型
  , QueryJobReq, QueryJobRsp                                        // 查询类型
  , DelReq                                                          // 删除类型
>
{
    /// <summary>
    ///     作业记录计数
    /// </summary>
    Task<long> CountRecordAsync(QueryReq<QueryJobRecordReq> req);

    /// <summary>
    ///     执行作业
    /// </summary>
    Task ExecuteAsync(QueryJobReq req);

    /// <summary>
    ///     导出作业记录
    /// </summary>
    Task<IActionResult> ExportRecordAsync(QueryReq<QueryJobRecordReq> req);

    /// <summary>
    ///     获取单个作业记录
    /// </summary>
    Task<QueryJobRecordRsp> GetRecordAsync(QueryJobRecordReq req);

    /// <summary>
    ///     获取作业记录条形图数据
    /// </summary>
    Task<IEnumerable<GetBarChartRsp>> GetRecordBarChartAsync(QueryReq<QueryJobRecordReq> req);

    /// <summary>
    ///     状态码分组作业记录饼图数据
    /// </summary>
    Task<IEnumerable<GetPieChartRsp>> GetRecordPieChartByHttpStatusCodeAsync(QueryReq<QueryJobRecordReq> req);

    /// <summary>
    ///     名称分组作业记录饼图数据
    /// </summary>
    Task<IEnumerable<GetPieChartRsp>> GetRecordPieChartByNameAsync(QueryReq<QueryJobRecordReq> req);

    /// <summary>
    ///     分页查询作业记录
    /// </summary>
    Task<PagedQueryRsp<QueryJobRecordRsp>> PagedQueryRecordAsync(PagedQueryReq<QueryJobRecordReq> req);

    /// <summary>
    ///     设置计划作业启用状态
    /// </summary>
    Task<int> SetEnabledAsync(SetJobEnabledReq req);
}