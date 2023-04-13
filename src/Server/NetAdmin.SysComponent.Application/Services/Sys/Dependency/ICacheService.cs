using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Sys.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     缓存服务
/// </summary>
public interface ICacheService : IService, ICacheModule
{
    /// <summary>
    ///     获取所有缓存项
    /// </summary>
    IEnumerable<GetAllEntriesRsp> GetAllEntries();
}