using System.Reflection;
using Mapster;
using Microsoft.Extensions.Caching.Memory;
using NetAdmin.Application.Services.Sys.Dependency;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="ICacheService" />
public class CacheService : ServiceBase<ICacheService>, ICacheService
{
    private readonly IMemoryCache _memoryCache;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CacheService" /> class.
    /// </summary>
    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    /// <inheritdoc />
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
    ///     GetAllEntrys
    /// </summary>
    public List<GetAllEntrysRsp> GetAllEntrys()
    {
        var ret = new List<GetAllEntrysRsp>();
        var coherentState = typeof(MemoryCache).GetRuntimeFields()
                                               .First(x => x.Name == "_coherentState")
                                               .GetValue(_memoryCache);

        var entriesCollection = coherentState?.GetType()
                                             .GetProperty( //
                                                 "EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance)
                                             ?.GetValue(coherentState);
        var entrys = entriesCollection?.GetType().GetProperty("Value")?.GetValue(entriesCollection);

        foreach (var entry in entrys?.GetType().GetRuntimeProperties()!) {
            Console.WriteLine(entry);
        }

        return ret;
    }
}