namespace NetAdmin.Domain.Dto.Sys.Cache;

/// <summary>
///     请求：获取缓存项
/// </summary>
public sealed record GetEntriesReq : DataAbstraction
{
    /// <summary>
    ///     缓存键
    /// </summary>
    public string Key { get; init; }
}