namespace NetAdmin.Domain.Attributes;

/// <summary>
///     标记一个金币类型为收入
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public sealed class IsIncomeAttribute : Attribute { }