namespace NetAdmin.Host.Extensions;

/// <summary>
///     工作单元管理器扩展方法
/// </summary>
public static class UnitOfWorkManagerExtensions
{
    /// <summary>
    ///     事务操作
    /// </summary>
    public static async Task AtomicOperateAsync(this UnitOfWorkManager me, Func<Task> handle)
    {
        var       logger     = LogHelper.Get<UnitOfWorkManager>();
        using var unitOfWork = me.Begin();
        var       hashCode   = unitOfWork.GetHashCode();
        try {
            #if DEBUG
            logger?.Debug($"{Ln.开始事务}: {hashCode}");
            #endif
            await handle().ConfigureAwait(false);
            unitOfWork.Commit();
            logger?.Info($"{Ln.事务已提交}: {hashCode}");
        }
        catch {
            unitOfWork.Rollback();
            logger?.Warn($"{Ln.事务已回滚}: {hashCode}");
            throw;
        }
    }
}