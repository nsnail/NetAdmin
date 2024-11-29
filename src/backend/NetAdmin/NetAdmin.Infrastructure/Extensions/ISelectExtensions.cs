﻿namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     ISelect 扩展方法
/// </summary>
public static class ISelectExtensions
{
    /// <summary>
    ///     无锁无等待
    /// </summary>
    public static ISelect<T1> WithNoLockNoWait<T1>(this ISelect<T1> me)
    {
        return me
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            ;
    }

    /// <summary>
    ///     无锁无等待
    /// </summary>
    public static ISelect<T1, T2> WithNoLockNoWait<T1, T2>(this ISelect<T1, T2> me)
        where T2 : class
    {
        return me
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            ;
    }

    /// <summary>
    ///     无锁无等待
    /// </summary>
    public static ISelect<T1, T2, T3> WithNoLockNoWait<T1, T2, T3>(this ISelect<T1, T2, T3> me)
        where T2 : class //
        where T3 : class
    {
        return me
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            ;
    }

    /// <summary>
    ///     无锁无等待
    /// </summary>
    public static ISelect<T1, T2, T3, T4> WithNoLockNoWait<T1, T2, T3, T4>(this ISelect<T1, T2, T3, T4> me)
        where T2 : class //
        where T3 : class //
        where T4 : class
    {
        return me
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            ;
    }

    /// <summary>
    ///     无锁无等待
    /// </summary>
    public static ISelect<T1, T2, T3, T4, T5> WithNoLockNoWait<T1, T2, T3, T4, T5>(this ISelect<T1, T2, T3, T4, T5> me)
        where T2 : class //
        where T3 : class //
        where T4 : class //
        where T5 : class
    {
        return me
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            ;
    }

    /// <summary>
    ///     无锁无等待
    /// </summary>
    public static ISelect<T1, T2, T3, T4, T5, T6> WithNoLockNoWait<T1, T2, T3, T4, T5, T6>(this ISelect<T1, T2, T3, T4, T5, T6> me)
        where T2 : class //
        where T3 : class //
        where T4 : class //
        where T5 : class //
        where T6 : class
    {
        return me
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            ;
    }
}