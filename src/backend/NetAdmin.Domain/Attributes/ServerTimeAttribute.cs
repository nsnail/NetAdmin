namespace NetAdmin.Domain.Attributes;

/// <summary>
///     标记一个字段启用服务器时间
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class ServerTimeAttribute : Attribute { }