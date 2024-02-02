using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.JobRecord;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     计划作业执行记录模块
/// </summary>
public interface IJobRecordModule : ICrudModule<CreateJobRecordReq, QueryJobRecordRsp // 创建类型
  , QueryJobRecordReq, QueryJobRecordRsp                                              // 查询类型
  , UpdateJobRecordReq, QueryJobRecordRsp                                             // 修改类型
  , DelReq                                                                            // 删除类型
>;