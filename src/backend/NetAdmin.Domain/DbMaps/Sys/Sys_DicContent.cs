using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     字典内容表
/// </summary>
[Index($"{Chars.FLG_DB_INDEX_PREFIX}{nameof(CatalogId)}_{nameof(Key)}",   $"{nameof(CatalogId)},{nameof(Key)}",   true)]
[Index($"{Chars.FLG_DB_INDEX_PREFIX}{nameof(CatalogId)}_{nameof(Value)}", $"{nameof(CatalogId)},{nameof(Value)}", true)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_DicContent))]
public record Sys_DicContent : VersionEntity
{
    /// <summary>
    ///     字典目录
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(CatalogId))]
    public Sys_DicCatalog Catalog { get; init; }

    /// <summary>
    ///     字典目录编号
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual long CatalogId { get; init; }

    /// <summary>
    ///     键名称
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [JsonIgnore]
    public virtual string Key { get; init; }

    /// <summary>
    ///     键值
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [JsonIgnore]
    public virtual string Value { get; init; }
}