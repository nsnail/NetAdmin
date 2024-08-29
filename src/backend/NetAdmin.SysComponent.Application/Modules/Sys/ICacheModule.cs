using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Cache;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     缓存模块
/// </summary>
public interface ICacheModule
{
    /// <summary>
    ///     批量删除缓存项
    /// </summary>
    Task<int> BulkDeleteEntryAsync(BulkReq<DelEntryReq> req);

    /// <summary>
    ///     缓存统计
    /// </summary>
    Task<CacheStatisticsRsp> CacheStatisticsAsync();

    /// <summary>
    ///     删除缓存项
    /// </summary>
    Task<int> DeleteEntryAsync(DelEntryReq req);

    /// <summary>
    ///     获取所有缓存项
    /// </summary>
    Task<IEnumerable<GetEntryRsp>> GetAllEntriesAsync(GetAllEntriesReq req);

    /// <summary>
    ///     获取缓存项
    /// </summary>
    Task<GetEntryRsp> GetEntryAsync(GetEntriesReq req);
}