using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     计划作业缓存
/// </summary>
public interface IJobCache : ICache<IDistributedCache, IJobService>, IJobModule;