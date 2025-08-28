using StackExchange.Redis;

namespace NetAdmin.Infrastructure.Utils;

/// <summary>
///     Redis 分布锁
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="RedisLocker" /> class.
/// </remarks>
#pragma warning disable DesignedForInheritance
public sealed class RedisLocker : IAsyncDisposable
#pragma warning restore DesignedForInheritance
{
    private readonly IDatabase _redisDatabase;

    private readonly string _redisKey;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RedisLocker" /> class.
    ///     Redis 分布锁
    /// </summary>
    private RedisLocker(
        IDatabase redisDatabase
        , string redisKey
    ) {
        _redisDatabase = redisDatabase;
        _redisKey = redisKey;
    }

    /// <summary>
    ///     获取锁
    /// </summary>
    /// <exception cref="NetAdminGetLockerException">NetAdminGetLockerException</exception>
    public static async Task<RedisLocker> GetLockerAsync(
        IDatabase redisDatabase
        , string lockerName
        , TimeSpan lockerExpire
        , int retryCount
        , TimeSpan retryDelay
    ) {
        lockerName = $"{nameof(RedisLocker)}.{lockerName}";
        var setOk = false;
        for (var i = 0; i != retryCount; ++i) {
            try {
                setOk = await redisDatabase
                    .StringSetAsync(lockerName, RedisValue.EmptyString, lockerExpire, When.NotExists, CommandFlags.DemandMaster)
                    .ConfigureAwait(false);
            }
            catch (Exception ex) {
                LogHelper.Get<RedisLocker>().Error(ex.Message);
            }

            if (setOk) {
                return new RedisLocker(redisDatabase, lockerName);
            }

            if (retryCount > 1) {
                await Task.Delay(retryDelay).ConfigureAwait(false);
            }
        }

        throw new NetAdminGetLockerException(lockerName);
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync() {
        try {
            _ = await _redisDatabase.KeyDeleteAsync(_redisKey, CommandFlags.DemandMaster | CommandFlags.FireAndForget).ConfigureAwait(false);
        }
        catch (Exception ex) {
            LogHelper.Get<RedisLocker>().Error(ex.Message);
        }
    }
}