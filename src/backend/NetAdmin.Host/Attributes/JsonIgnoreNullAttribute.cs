namespace NetAdmin.Host.Attributes;

/// <summary>
///     标记一个方法结果在序列化时忽略null值
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class JsonIgnoreNullAttribute : Attribute { }