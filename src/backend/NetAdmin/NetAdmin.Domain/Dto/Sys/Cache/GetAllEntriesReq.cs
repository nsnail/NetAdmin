namespace NetAdmin.Domain.Dto.Sys.Cache;

/// <summary>
///     请求：获取所有缓存项
/// </summary>
public sealed record GetAllEntriesReq : DataAbstraction
{
    /// <summary>
    ///     关键词
    /// </summary>
    public string Keywords { get; init; }
}