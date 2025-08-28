using NetAdmin.Application.Repositories;
using NetAdmin.Domain.DbMaps.Dependency;
using StackExchange.Redis;

namespace NetAdmin.Application.Services;

/// <summary>
///     Redis Service Base
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="RedisService{TEntity, TPrimary, TLogger}" /> class.
///     Redis Service Base
/// </remarks>
public abstract class RedisService<TEntity, TPrimary, TLogger>(BasicRepository<TEntity, TPrimary> rpo)
    : RepositoryService<TEntity, TPrimary, TLogger>(rpo)
    where TEntity : EntityBase<TPrimary>
    where TPrimary : IEquatable<TPrimary>
{
    /// <summary>
    ///     Redis Database
    /// </summary>
    protected IDatabase RedisDatabase { get; } = App
        .GetService<IConnectionMultiplexer>()
        .GetDatabase(App.GetOptions<RedisOptions>().Instances.First(x => x.Name == Chars.FLG_REDIS_INSTANCE_DATA_CACHE).Database);

    /// <summary>
    ///     获取锁
    /// </summary>
    protected Task<RedisLocker> GetLockerAsync(string lockerName) {
        return RedisLocker.GetLockerAsync(
            RedisDatabase, lockerName, TimeSpan.FromSeconds(Numbers.SECS_REDIS_LOCK_EXPIRY), Numbers.MAX_LIMIT_RETRY_CNT_REDIS_LOCK
            , TimeSpan.FromSeconds(Numbers.SECS_REDIS_LOCK_RETRY_DELAY)
        );
    }

    /// <summary>
    ///     获取锁（仅获取一次）
    /// </summary>
    protected Task<RedisLocker> GetLockerOnceAsync(string lockerName) {
        return RedisLocker.GetLockerAsync(
            RedisDatabase, lockerName, TimeSpan.FromSeconds(Numbers.SECS_REDIS_LOCK_EXPIRY), 1
            , TimeSpan.FromSeconds(Numbers.SECS_REDIS_LOCK_RETRY_DELAY)
        );
    }
}