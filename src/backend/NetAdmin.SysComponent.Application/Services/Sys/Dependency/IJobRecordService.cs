using NetAdmin.Application.Services;
using NetAdmin.SysComponent.Application.Modules.Sys;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     计划作业执行记录服务
/// </summary>
public interface IJobRecordService : IService, IJobRecordModule;