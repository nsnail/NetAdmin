using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Sys.Job;
using NetAdmin.SysComponent.Application.Modules.Sys;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     计划作业服务
/// </summary>
public interface IJobService : IService, IJobModule
{
    /// <summary>
    ///     完成计划作业
    /// </summary>
    Task FinishJobAsync(FinishJobReq req);

    /// <summary>
    ///     获取下一个要执行的计划作业
    /// </summary>
    Task<QueryJobRsp> GetNextJobAsync();

    /// <summary>
    ///     释放卡死的任务
    /// </summary>
    Task<int> ReleaseStuckTaskAsync();
}