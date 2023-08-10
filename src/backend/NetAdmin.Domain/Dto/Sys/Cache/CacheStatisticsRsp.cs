namespace NetAdmin.Domain.Dto.Sys.Cache;

/// <summary>
///     响应：缓存统计
/// </summary>
public sealed record CacheStatisticsRsp : DataAbstraction
{
    private static readonly Regex[] _regexes = {
                                                   new(@"keyspace_hits:(\d+)", RegexOptions.Compiled)
                                                 , new(@"keyspace_misses:(\d+)", RegexOptions.Compiled)
                                                 , new(@"uptime_in_seconds:(\d+)", RegexOptions.Compiled)
                                                 , new(@"used_cpu_sys:([\d\\.]+)", RegexOptions.Compiled)
                                                 , new(@"used_cpu_user:([\d\\.]+)", RegexOptions.Compiled)
                                                 , new(@"used_memory:(\d+)", RegexOptions.Compiled)
                                                 , new("redis_version:(.+)", RegexOptions.Compiled)
                                               };

    /// <summary>
    ///     Initializes a new instance of the <see cref="CacheStatisticsRsp" /> class.
    /// </summary>
    public CacheStatisticsRsp() { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="CacheStatisticsRsp" /> class.
    /// </summary>
    public CacheStatisticsRsp(string redisResult)
    {
        KeyspaceHits   = _regexes[0].Match(redisResult).Groups[1].Value.Trim().Int64Try(0);
        KeyspaceMisses = _regexes[1].Match(redisResult).Groups[1].Value.Trim().Int64Try(0);
        UpTime         = _regexes[2].Match(redisResult).Groups[1].Value.Trim().Int64Try(0);
        UsedCpu = _regexes[3].Match(redisResult).Groups[1].Value.Trim().DecTry(0) +
                  _regexes[4].Match(redisResult).Groups[1].Value.Trim().DecTry(0);
        UsedMemory = _regexes[5].Match(redisResult).Groups[1].Value.Trim().Int64Try(0);
        Version    = _regexes[6].Match(redisResult).Groups[1].Value.Trim();
    }

    /// <summary>
    ///     键总数
    /// </summary>
    public long DbSize { get; init; }

    /// <summary>
    ///     命中键的数量
    /// </summary>
    public long KeyspaceHits { get; init; }

    /// <summary>
    ///     未命中键的数量
    /// </summary>
    public long KeyspaceMisses { get; init; }

    /// <summary>
    ///     Redis运行时间（秒）
    /// </summary>
    public long UpTime { get; init; }

    /// <summary>
    ///     使用的CPU时间（秒）
    /// </summary>
    public decimal UsedCpu { get; init; }

    /// <summary>
    ///     使用的内存量（字节）
    /// </summary>
    public long UsedMemory { get; init; }

    /// <summary>
    ///     Redis版本号
    /// </summary>
    public string Version { get; init; }
}