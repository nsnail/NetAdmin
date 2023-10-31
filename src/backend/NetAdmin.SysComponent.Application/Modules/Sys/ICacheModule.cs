using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Cache;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     缓存模块
/// </summary>
public interface ICacheModule
{
    /// <summary>
    ///     缓存统计
    /// </summary>
    Task<CacheStatisticsRsp> CacheStatisticsAsync();

    /// <summary>
    ///     获取所有缓存项
    /// </summary>
    Task<PagedQueryRsp<GetAllEntriesRsp>> GetAllEntries(PagedQueryReq<GetAllEntriesReq> req);
}