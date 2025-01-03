using StackExchange.Redis;

namespace NetAdmin.Domain.Dto.Sys.Cache;

/// <summary>
///     响应：获取所有缓存项
/// </summary>
public sealed record GetEntryRsp : DataAbstraction
{
    /// <summary>
    ///     缓存值
    /// </summary>
    public string Data { get; set; }

    /// <summary>
    ///     过期时间
    /// </summary>
    public DateTime? ExpireTime { get; init; }

    /// <summary>
    ///     缓存键
    /// </summary>
    public string Key { get; init; }

    /// <summary>
    ///     数据类型
    /// </summary>
    public RedisType Type { get; init; }
}