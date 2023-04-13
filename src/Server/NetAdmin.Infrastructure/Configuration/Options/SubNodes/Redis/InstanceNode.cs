namespace NetAdmin.Infrastructure.Configuration.Options.SubNodes.Redis;

/// <summary>
///     Redis实例
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
    public int Database { get; set; }

    /// <summary>
    ///     实例名称
    /// </summary>
    public string Name { get; set; }
}