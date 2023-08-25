namespace NetAdmin.Domain.Dto.Sys.Cache;

/// <summary>
///     请求：获取所有缓存项
/// </summary>
public sealed record GetAllEntriesReq : DataAbstraction
{
    /// <summary>
    ///     数据库索引号
    /// </summary>
    public uint DbIndex { get; init; }
}