namespace NetAdmin.Domain.Dto.Sys.Cache;

/// <summary>
///     响应：获取所有缓存项
/// </summary>
public record GetAllEntrysRsp : DataAbstraction
{
    /// <summary>
    ///     绝对过期时间
    /// </summary>
    public DateTime? AbsoluteExpiration { get; init; }

    /// <summary>
    ///     缓存Key
    /// </summary>
    public string Key { get; init; }

    /// <summary>
    ///     最后访问时间
    /// </summary>
    public DateTime? LastAccessed { get; init; }

    /// <summary>
    ///     滑动过期时间间隔
    /// </summary>
    public TimeSpan? SlidingExpiration { get; init; }
}