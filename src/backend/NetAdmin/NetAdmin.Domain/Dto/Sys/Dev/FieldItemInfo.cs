namespace NetAdmin.Domain.Dto.Sys.Dev;

/// <summary>
///     信息：字段项信息
/// </summary>
public sealed record FieldItemInfo : DataAbstraction
{
    /// <summary>
    ///     数据库字段类型
    /// </summary>
    public string DbType { get; init; }

    /// <summary>
    ///     代码模板
    /// </summary>
    public string Example { get; init; }

    /// <summary>
    ///     可空
    /// </summary>
    public bool IsNullable { get; init; }

    /// <summary>
    ///     是否主键
    /// </summary>
    public bool IsPrimary { get; set; }

    /// <summary>
    ///     值类型
    /// </summary>
    public bool IsStruct { get; init; }

    /// <summary>
    ///     名称
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    ///     备注
    /// </summary>
    public string Summary { get; init; }

    /// <summary>
    ///     类型
    /// </summary>
    public string Type { get; init; }
}