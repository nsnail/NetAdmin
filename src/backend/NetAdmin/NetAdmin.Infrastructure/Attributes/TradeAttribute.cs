namespace NetAdmin.Infrastructure.Attributes;

/// <summary>
///     交易方向特性
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
public sealed class TradeAttribute : Attribute
{
    /// <summary>
    ///     交易方向
    /// </summary>
    public TradeDirections Direction { get; init; }
}