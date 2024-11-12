namespace NetAdmin.Infrastructure.Configuration.Options.SubNodes.Redis;

/// <summary>
///     Redis 实例节点
/// </summary>
public sealed record InstanceNode
{
    /// <summary>
    ///     链接字符串
    /// </summary>
    public string ConnStr { get; init; }

    /// <summary>
    ///     数据库
    /// </summary>
    public int Database { get; init; }

    /// <summary>
    ///     实例名称
    /// </summary>
    public string Name { get; init; }
}