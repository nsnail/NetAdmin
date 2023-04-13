// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace NetAdmin.Domain.Attributes;

/// <summary>
///     标记一个字段启用雪花id生成
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class SnowflakeAttribute : Attribute { }