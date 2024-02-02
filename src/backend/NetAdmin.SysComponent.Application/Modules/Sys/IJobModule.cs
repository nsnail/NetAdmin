using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Job;

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
    ///     启用/禁用作业
    /// </summary>
    Task SetEnabledAsync(UpdateJobReq req);
}