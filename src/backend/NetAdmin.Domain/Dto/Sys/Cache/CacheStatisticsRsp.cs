namespace NetAdmin.Domain.Dto.Sys.Cache;

/// <summary>
///     响应：缓存统计
/// </summary>
public record CacheStatisticsRsp : DataAbstraction
{
    /// <summary>
    ///     获取内存缓存中当前实例的数目
    /// </summary>
    public long CurrentEntryCount { get; init; }

    /// <summary>
    ///     获取内存缓存中当前所有 Size 值的估计和。
    /// </summary>
    public long? CurrentEstimatedSize { get; init; }

    /// <summary>
    ///     获取缓存命中总数。
    /// </summary>
    public long TotalHits { get; init; }

    /// <summary>
    ///     获取缓存未命中总数。
    /// </summary>
    public long TotalMisses { get; init; }
}