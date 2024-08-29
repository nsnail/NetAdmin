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
    public static readonly string ProductVersion
        = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly()!.Location).ProductVersion;

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
    public static DateTime LatestLogTime => Volatile.Read(ref _latestLogTime).Time();

    /// <summary>
    ///     日志记录器忽略的API编号
    /// </summary>
    public static string[] LoggerIgnoreApiIds => [
        "api/adm/tools/query.es.log", "api/probe/health.check", "api/probe/is.system.safety.stopped"
    ];

    /// <summary>
    ///     系统内部密钥
    /// </summary>
    public static string SecretKey => "{6C4922D3-499A-46db-BFC4-0B51A9C4395F}";

    /// <summary>
    ///     SQL 随机排序语法
    /// </summary>
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public static string SqlRandomSorting =>
        App.GetOptions<DatabaseOptions>().DbType switch {
            DataType.MySql            => "RAND()"
          , DataType.SqlServer        => "NEWID()"
          , DataType.PostgreSQL       => "RANDOM()"
          , DataType.Oracle           => "DBMS_RANDOM.value"
          , DataType.Sqlite           => "RANDOM()"
          , DataType.OdbcOracle       => throw new NotImplementedException()
          , DataType.OdbcSqlServer    => throw new NotImplementedException()
          , DataType.OdbcMySql        => throw new NotImplementedException()
          , DataType.OdbcPostgreSQL   => throw new NotImplementedException()
          , DataType.Odbc             => throw new NotImplementedException()
          , DataType.OdbcDameng       => throw new NotImplementedException()
          , DataType.MsAccess         => throw new NotImplementedException()
          , DataType.Dameng           => throw new NotImplementedException()
          , DataType.OdbcKingbaseES   => throw new NotImplementedException()
          , DataType.ShenTong         => throw new NotImplementedException()
          , DataType.KingbaseES       => throw new NotImplementedException()
          , DataType.Firebird         => throw new NotImplementedException()
          , DataType.Custom           => throw new NotImplementedException()
          , DataType.ClickHouse       => throw new NotImplementedException()
          , DataType.GBase            => throw new NotImplementedException()
          , DataType.QuestDb          => throw new NotImplementedException()
          , DataType.Xugu             => throw new NotImplementedException()
          , DataType.CustomOracle     => throw new NotImplementedException()
          , DataType.CustomSqlServer  => throw new NotImplementedException()
          , DataType.CustomMySql      => throw new NotImplementedException()
          , DataType.CustomPostgreSQL => throw new NotImplementedException()
          , _                         => throw new NotImplementedException()
        };

    /// <summary>
    ///     Json序列化选项
    /// </summary>
    public static JsonSerializerOptions JsonSerializerOptions { get; set; }

    /// <summary>
    ///     增加日志计数器
    /// </summary>
    public static void IncrementLogCounter()
    {
        Volatile.Write(ref _latestLogTime, DateTime.Now.TimeUnixUtcMs());
    }
}