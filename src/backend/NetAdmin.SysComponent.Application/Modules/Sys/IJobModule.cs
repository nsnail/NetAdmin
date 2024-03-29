using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Job;
using NetAdmin.Domain.Dto.Sys.JobRecord;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     计划作业模块
/// </summary>
public interface IJobModule : ICrudModule<CreateJobReq, QueryJobRsp // 创建类型
  , QueryJobReq, QueryJobRsp                                        // 查询类型
  , UpdateJobReq, QueryJobRsp                                       // 修改类型
  , DelReq                                                          // 删除类型
>
{
    /// <summary>
    ///     编辑作业
    /// </summary>
    Task<QueryJobRsp> EditAsync(UpdateJobReq req);

    /// <summary>
    ///     获取单个作业记录
    /// </summary>
    Task<QueryJobRecordRsp> RecordGetAsync(QueryJobRecordReq req);

    /// <summary>
    ///     分页查询作业记录
    /// </summary>
    Task<PagedQueryRsp<QueryJobRecordRsp>> RecordPagedQueryAsync(PagedQueryReq<QueryJobRecordReq> req);

    /// <summary>
    ///     启用/禁用作业
    /// </summary>
    Task SetEnabledAsync(UpdateJobReq req);
}