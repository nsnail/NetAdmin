namespace NetAdmin.Domain.Attributes;

/// <summary>
///     危险字段标记
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class DangerFieldAttribute : Attribute;