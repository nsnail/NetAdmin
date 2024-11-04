using NetAdmin.Infrastructure.Configuration.Dependency;

namespace NetAdmin.Infrastructure.Configuration.Options;

/// <summary>
///     Redis 配置
/// </summary>
public sealed record RedisOptions : OptionAbstraction
{
    /// <summary>
    ///     实例列表
    /// </summary>
    public InstanceNode[] Instances { get; init; }
}