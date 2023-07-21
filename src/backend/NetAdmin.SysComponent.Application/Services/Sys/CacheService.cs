using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Sys.Cache;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ICacheService" />
public sealed class CacheService : ServiceBase<ICacheService>, ICacheService
{
    private readonly IMemoryCache _memoryCache;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CacheService" /> class.
    /// </summary>
    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    /// <summary>
    ///     缓存统计
    /// </summary>
    public CacheStatisticsRsp CacheStatistics()
    {
        return _memoryCache.GetCurrentStatistics()?.Adapt<CacheStatisticsRsp>();
    }

    /// <summary>
    ///     清空缓存
    /// </summary>
    public void Clear()
    {
        (_memoryCache as MemoryCache)?.Clear();
    }

    /// <summary>
    ///     获取所有缓存项
    /// </summary>
    public IEnumerable<GetAllEntriesRsp> GetAllEntries()
    {
        var coherentState = typeof(MemoryCache).GetRuntimeFields()
                                               .First(x => x.Name == "_coherentState")
                                               .GetValue(_memoryCache);

        var entriesCollection = coherentState?.GetType()
                                             .GetProperty( //
                                                 #pragma warning disable S3011
                                                 "EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance)
                                             #pragma warning restore S3011
                                             ?.GetValue(coherentState);
        var entries = entriesCollection?.GetType().GetProperty("Value")?.GetValue(entriesCollection);

        foreach (var entry in entries?.GetType().GetRuntimeProperties()!) {
            Logger.Debug(entry);
        }

        return new List<GetAllEntriesRsp>();
    }
}