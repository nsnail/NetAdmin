namespace NetAdmin.Domain.Attributes;

/// <summary>
///     枚举装饰
/// </summary>
/// <inheritdoc />
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
public sealed class EnumDecorationAttribute(string indicate = nameof(Indicates.None), bool pulse = false, int sort = 0) : Attribute
{
    /// <summary>
    ///     状态指示
    /// </summary>
    public string Indicate { get; } = indicate;

    /// <summary>
    ///     脉动
    /// </summary>
    public bool Pulse { get; } = pulse;

    /// <summary>
    ///     排序值
    /// </summary>
    public int Sort { get; } = sort;
}