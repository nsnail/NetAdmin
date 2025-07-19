namespace NetAdmin.Domain.Dto.Dependency;

/// <summary>
///     工作批请求
/// </summary>
public record JobReq : DataAbstraction
{
    /// <summary>
    ///     处理数量
    /// </summary>
    public int? Count { get; init; }

    /// <summary>
    ///     n秒以前
    /// </summary>
    public int? SecondsAgo { get; init; }

    /// <summary>
    ///     直到n秒前
    /// </summary>
    public int? UntilSecondsAgo { get; init; }
}