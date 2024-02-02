using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     计划作业执行记录缓存
/// </summary>
public interface IJobRecordCache : ICache<IDistributedCache, IJobRecordService>, IJobRecordModule;