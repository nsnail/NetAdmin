namespace NetAdmin.Host.Extensions;

/// <summary>
///     工作单元管理器扩展类
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
            logger?.Debug($"{Ln.Transaction_starting}: {hashCode}");
            #endif
            await handle();
            unitOfWork.Commit();
            logger?.Info($"{Ln.Transaction_commited}: {hashCode}");
        }
        catch {
            unitOfWork.Rollback();
            logger?.Error($"{Ln.Transaction_rollbacked}: {hashCode}");
            throw;
        }
    }
}