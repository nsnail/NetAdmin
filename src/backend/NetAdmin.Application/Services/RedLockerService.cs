using NetAdmin.Application.Repositories;
using RedLockNet;

namespace NetAdmin.Application.Services;

/// <summary>
///     RedLocker Service Base
/// </summary>
public abstract class RedLockerService<TEntity, TPrimary, TLogger>(
    BasicRepository<TEntity, TPrimary> rpo
  , RedLocker                          redLocker) : RepositoryService<TEntity, TPrimary, TLogger>(rpo)
    where TEntity : EntityBase<TPrimary> //
    where TPrimary : IEquatable<TPrimary>
{
    /// <summary>
    ///     获取锁
    /// </summary>
    protected Task<IRedLock> GetLockerAsync(string lockName)
    {
        return GetLockerAsync(lockName, TimeSpan.FromSeconds(Numbers.SECS_RED_LOCK_WAIT)
                  ,                     TimeSpan.FromSeconds(Numbers.SECS_RED_LOCK_RETRY_INTERVAL));
    }

    /// <summary>
    ///     获取锁
    /// </summary>
    /// <exception cref="NetAdminGetLockerException">NetAdminGetLockerException</exception>
    protected async Task<IRedLock> GetLockerAsync(string lockName, TimeSpan waitTime, TimeSpan retryInterval)
    {
        // 加锁
        var lockTask = waitTime == default || retryInterval == default
            ? redLocker.RedLockFactory.CreateLockAsync(lockName, TimeSpan.FromSeconds(Numbers.SECS_RED_LOCK_EXPIRY))
            : redLocker.RedLockFactory.CreateLockAsync( //
                lockName, TimeSpan.FromSeconds(Numbers.SECS_RED_LOCK_EXPIRY), waitTime, retryInterval);
        var redLock = await lockTask.ConfigureAwait(false);

        return redLock.IsAcquired ? redLock : throw new NetAdminGetLockerException();
    }

    /// <summary>
    ///     获取锁
    /// </summary>
    /// <remarks>
    ///     不重试，失败直接抛出异常
    /// </remarks>
    protected Task<IRedLock> GetLockerOnceAsync(string lockName)
    {
        return GetLockerAsync(lockName, default, default);
    }
}