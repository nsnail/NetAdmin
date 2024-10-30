namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     字典内容表
/// </summary>
[SqlIndex($"{Chars.FLG_DB_INDEX_PREFIX}{nameof(CatalogId)}_{nameof(Key)}", $"{nameof(CatalogId)},{nameof(Key)}", true)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_DicContent))]
public record Sys_DicContent : VersionEntity, IFieldEnabled, IFieldSummary
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

    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual bool Enabled { get; init; }

    /// <summary>
    ///     键名称
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Key { get; init; }

    /// <summary>
    ///     描述
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Summary { get; init; }

    /// <summary>
    ///     键值
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Value { get; init; }
}