// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace NetAdmin.DataContract.Attributes;

/// <summary>
///     标记一个字段是否启用雪花id生成
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class SnowflakeAttribute : Attribute
{
    /// <summary>
    ///     启用
    /// </summary>
    public bool Enable { get; init; } = true;
}