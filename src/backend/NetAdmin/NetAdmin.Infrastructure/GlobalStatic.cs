using DataType = FreeSql.DataType;

namespace NetAdmin.Infrastructure;

/// <summary>
///     全局静态类
/// </summary>
public static class GlobalStatic
{
    /// <summary>
    ///     当前进程
    /// </summary>
    public static readonly Process CurrentProcess = Process.GetCurrentProcess();

    /// <summary>
    ///     产品版本
    /// </summary>
    public static readonly string ProductVersion = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly()!.Location).ProductVersion;

    private static long _latestLogTime;

    /// <summary>
    ///     调试模式
    /// </summary>
    public static bool DebugMode =>
        #if DEBUG
        true
    #else
        false
    #endif
    ;

    /// <summary>
    ///     最后一次日志时间
    /// </summary>
    public static DateTime LatestLogTime => LogCounterOff ? DateTime.MinValue : Volatile.Read(ref _latestLogTime).Time();

    /// <summary>
    ///     系统内部密钥
    /// </summary>
    public static string SecretKey => "{6C4922D3-499A-46db-BFC4-0B51A9C4395F}";

    /// <summary>
    ///     SQL 随机排序语法
    /// </summary>
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public static string SqlRandomSorting =>
        App.GetOptions<DatabaseOptions>().DbType switch
        {
            DataType.MySql => "RAND()"
            , DataType.SqlServer => "NEWID()"
            , DataType.PostgreSQL => "RANDOM()"
            , DataType.Oracle => "DBMS_RANDOM.value"
            , DataType.Sqlite => "RANDOM()"
            , _ => throw new NotImplementedException()
        };

    /// <summary>
    ///     Json序列化选项
    /// </summary>
    public static JsonSerializerOptions JsonSerializerOptions { get; set; }

    /// <summary>
    ///     停止更新日志时间
    /// </summary>
    public static bool LogCounterOff { get; set; }

    /// <summary>
    ///     增加日志计数器
    /// </summary>
    public static void IncrementLogCounter() {
        Volatile.Write(ref _latestLogTime, DateTime.Now.TimeUnixUtcMs());
    }
}