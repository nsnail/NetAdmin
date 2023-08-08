using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Sys.Cache;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using StackExchange.Redis;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ICacheService" />
public sealed class CacheService : ServiceBase<ICacheService>, ICacheService
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CacheService" /> class.
    /// </summary>
    public CacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    /// <summary>
    ///     缓存统计
    /// </summary>
    public Task<CacheStatisticsRsp> CacheStatisticsAsync()
    {
        return Task.FromResult(new CacheStatisticsRsp((string)_connectionMultiplexer.GetDatabase().Execute("INFO")));
    }

    /// <summary>
    ///     清空缓存
    /// </summary>
    public void Clear()
    {
        Console.WriteLine(GetAllKeys(_connectionMultiplexer.GetDatabase()));

        // (_connectionMultiplexer as MemoryCache)?.Clear();
    }

    /// <summary>
    ///     获取所有缓存项
    /// </summary>
    public IEnumerable<GetAllEntriesRsp> GetAllEntries()
    {
        var coherentState = typeof(MemoryCache).GetRuntimeFields()
                                               .First(x => x.Name == "_coherentState")
                                               .GetValue(_connectionMultiplexer);

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

    private static IEnumerable<string> GetAllKeys(IDatabase db)
    {
        long nextCursor = 0;
        do {
            var redisResults = (RedisResult[])db.Execute("SCAN", nextCursor.ToInvString(), "COUNT", 1000);
            nextCursor = ((string)redisResults![0]).Int64();

            foreach (var key in ((string[])redisResults[1])!) {
                yield return key;
            }
        } while (nextCursor != 0);
    }
}