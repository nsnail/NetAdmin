using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     字典内容表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_DicContent))]
[Index($"idx_{{tablename}}_{nameof(CatalogId)}_{nameof(Key)}",   $"{nameof(CatalogId)},{nameof(Key)}",   true)]
[Index($"idx_{{tablename}}_{nameof(CatalogId)}_{nameof(Value)}", $"{nameof(CatalogId)},{nameof(Value)}", true)]
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
    [JsonIgnore]
    [Column]
    public virtual long CatalogId { get; init; }

    /// <summary>
    ///     键名称
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    public virtual string Key { get; init; }

    /// <summary>
    ///     键值
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    public virtual string Value { get; init; }
}