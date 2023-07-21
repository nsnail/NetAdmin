namespace NetAdmin.Host.Attributes;

/// <summary>
///     标记一个Action，其响应的json结果会被删除值为null的节点
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class RemoveNullNodeAttribute : Attribute { }