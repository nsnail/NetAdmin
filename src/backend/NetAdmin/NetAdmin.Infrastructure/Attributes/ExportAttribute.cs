namespace NetAdmin.Infrastructure.Attributes;

/// <summary>
///     标记一个此字段（枚举）将通过接口暴露到前端
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
public sealed class ExportAttribute : Attribute;