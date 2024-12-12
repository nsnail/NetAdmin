namespace NetAdmin.Domain.Attributes;

/// <summary>
///     标记一个枚举的状态指示
/// </summary>
/// <inheritdoc />
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
public sealed class IndicatorAttribute(string indicate, bool pulse) : Attribute
{
    /// <summary>
    ///     状态指示
    /// </summary>
    public string Indicate { get; } = indicate;

    /// <summary>
    ///     脉动
    /// </summary>
    public bool Pulse { get; } = pulse;
}