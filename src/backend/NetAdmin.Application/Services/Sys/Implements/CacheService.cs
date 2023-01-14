using System.Reflection;
using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NetAdmin.DataContract.Dto.Sys.Cache;

namespace NetAdmin.Application.Services.Sys.Implements;

/// <inheritdoc cref="NetAdmin.Application.Services.Sys.ICacheService" />
public class CacheService : ServiceBase<ICacheService>, ICacheService, IDynamicApiController
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
    [NonAction]
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