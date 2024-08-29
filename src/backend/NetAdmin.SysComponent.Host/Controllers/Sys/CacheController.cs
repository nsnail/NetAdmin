using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Cache;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     缓存服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class CacheController(ICacheCache cache) : ControllerBase<ICacheCache, ICacheService>(cache), ICacheModule
{
    /// <summary>
    ///     批量删除缓存项
    /// </summary>
    public Task<int> BulkDeleteEntryAsync(BulkReq<DelEntryReq> req)
    {
        return Cache.BulkDeleteEntryAsync(req);
    }

    /// <summary>
    ///     缓存统计
    /// </summary>
    public Task<CacheStatisticsRsp> CacheStatisticsAsync()
    {
        return Cache.CacheStatisticsAsync();
    }

    /// <summary>
    ///     删除缓存项
    /// </summary>
    public Task<int> DeleteEntryAsync(DelEntryReq req)
    {
        return Cache.DeleteEntryAsync(req);
    }

    /// <summary>
    ///     获取所有缓存项
    /// </summary>
    public Task<IEnumerable<GetEntryRsp>> GetAllEntriesAsync(GetAllEntriesReq req)
    {
        return Cache.GetAllEntriesAsync(req);
    }

    /// <summary>
    ///     获取缓存项
    /// </summary>
    public Task<GetEntryRsp> GetEntryAsync(GetEntriesReq req)
    {
        return Cache.GetEntryAsync(req);
    }
}