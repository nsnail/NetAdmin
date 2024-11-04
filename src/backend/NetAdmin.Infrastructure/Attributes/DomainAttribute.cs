namespace NetAdmin.Infrastructure.Attributes;

/// <summary>
///     域名特性
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
public sealed class DomainAttribute : Attribute
{
    /// <summary>
    ///     主机名称
    /// </summary>
    public string HostName { get; init; }
}