using System.Collections.Concurrent;
using NetAdmin.SysComponent.Domain.Dto.Sys.Cache;
using StackExchange.Redis;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ICacheService" />
public sealed class CacheService(IConnectionMultiplexer connectionMultiplexer) //
    : ServiceBase<ICacheService>, ICacheService
{
    private readonly InstanceNode _redisInstance;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CacheService" /> class.
    /// </summary>
    public CacheService(IConnectionMultiplexer connectionMultiplexer, IOptions<RedisOptions> redisOptions) //
        : this(connectionMultiplexer)                                                                      //
    {
        _redisInstance = redisOptions.Value.Instances.First(x => x.Name == Chars.FLG_REDIS_INSTANCE_DATA_CACHE);
    }

    /// <inheritdoc />
    public async Task<int> BulkDeleteEntryAsync(BulkReq<DelEntryReq> req)
    {
        req.ThrowIfInvalid();
        var ret = 0;

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var item in req.Items) {
            ret += await DeleteEntryAsync(item).ConfigureAwait(false);
        }

        return ret;
    }

    /// <inheritdoc />
    public async Task<CacheStatisticsRsp> CacheStatisticsAsync()
    {
        var database = connectionMultiplexer.GetDatabase(_redisInstance.Database);
        return new CacheStatisticsRsp((string)await database.ExecuteAsync("info").ConfigureAwait(false)) {
                   DbSize = (long)await database.ExecuteAsync("dbSize").ConfigureAwait(false)
               };
    }

    /// <inheritdoc />
    public async Task<int> DeleteEntryAsync(DelEntryReq req)
    {
        req.ThrowIfInvalid();
        #pragma warning disable VSTHRD103
        var database   = connectionMultiplexer.GetDatabase(_redisInstance.Database);
        var delSuccess = await database.KeyDeleteAsync(req.Key).ConfigureAwait(false);
        return delSuccess ? 1 : 0;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<GetEntryRsp>> GetAllEntriesAsync(GetAllEntriesReq req)
    {
        req.ThrowIfInvalid();
        #pragma warning disable VSTHRD103
        var server = connectionMultiplexer.GetServers()[0];

        var database = connectionMultiplexer.GetDatabase(_redisInstance.Database);
        var keys = server.Keys(_redisInstance.Database, $"*{req.Keywords}*", Numbers.MAX_LIMIT_BULK_REQ).Take(Numbers.MAX_LIMIT_BULK_REQ).ToList();
        #pragma warning restore VSTHRD103

        var dic = new ConcurrentDictionary<string, (DateTime?, RedisType)>();

        await Parallel.ForEachAsync(
                          keys
                        , async (key, _) =>
                              dic.TryAdd(
                                  key
                                , (DateTime.Now + await database.KeyTimeToLiveAsync(key).ConfigureAwait(false)
                                 , await database.KeyTypeAsync(key).ConfigureAwait(false))))
                      .ConfigureAwait(false);
        return dic.Select(x => new GetEntryRsp { Key = x.Key, ExpireTime = x.Value.Item1, Type = x.Value.Item2 });
    }

    /// <inheritdoc />
    public async Task<GetEntryRsp> GetEntryAsync(GetEntriesReq req)
    {
        req.ThrowIfInvalid();
        var database = connectionMultiplexer.GetDatabase(_redisInstance.Database);

        var ret = new GetEntryRsp {
                                      Type       = await database.KeyTypeAsync(req.Key).ConfigureAwait(false)
                                    , Key        = req.Key
                                    , ExpireTime = DateTime.Now + await database.KeyTimeToLiveAsync(req.Key).ConfigureAwait(false)
                                  };

        #pragma warning disable IDE0072
        ret.Data = ret.Type switch
                   #pragma warning restore IDE0072
                   {
                       RedisType.String    => await database.StringGetAsync(req.Key).ConfigureAwait(false)
                     , RedisType.List      => string.Join(", ", await database.ListRangeAsync(req.Key).ConfigureAwait(false))
                     , RedisType.Set       => string.Join(", ", await database.SetMembersAsync(req.Key).ConfigureAwait(false))
                     , RedisType.SortedSet => string.Join(", ", await database.SortedSetRangeByRankAsync(req.Key).ConfigureAwait(false))
                     , RedisType.Hash      => string.Join(", ", await database.HashGetAllAsync(req.Key).ConfigureAwait(false))
                     , _                   => "Unsupported key type"
                   };

        return ret;
    }
}