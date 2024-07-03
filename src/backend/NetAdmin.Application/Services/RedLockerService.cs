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
    /// <exception cref="NetAdminGetLockerException">NetAdminGetLockerException</exception>
    protected async Task<IRedLock> GetLockerAsync(string lockName)
    {
        // 加锁
        var redLock = await redLocker.RedLockFactory.CreateLockAsync( //
                                         lockName, TimeSpan.FromSeconds(Numbers.SECS_RED_LOCK_EXPIRY)
                                       , TimeSpan.FromSeconds(Numbers.SECS_RED_LOCK_WAIT)
                                       , TimeSpan.FromSeconds(Numbers.SECS_RED_LOCK_RETRY))
                                     .ConfigureAwait(false);

        return redLock.IsAcquired ? redLock : throw new NetAdminGetLockerException();
    }
}