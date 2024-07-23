namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     字典内容表
/// </summary>
[SqlIndex($"{Chars.FLG_DB_INDEX_PREFIX}{nameof(CatalogId)}_{nameof(Key)}", $"{nameof(CatalogId)},{nameof(Key)}", true)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_DicContent))]
public record Sys_DicContent : VersionEntity
{
    /// <summary>
    ///     字典目录
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(nameof(CatalogId))]
    public Sys_DicCatalog Catalog { get; init; }

    /// <summary>
    ///     字典目录编号
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long CatalogId { get; init; }

    /// <summary>
    ///     键名称
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Key { get; init; }

    /// <summary>
    ///     键值
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Value { get; init; }
}