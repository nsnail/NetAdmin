namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     字典目录表
/// </summary>
[FreeSql.DataAnnotations.Index(Chars.FLG_DB_INDEX_PREFIX + nameof(Code), nameof(Code), true)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX             + nameof(Sys_DicCatalog))]
public record Sys_DicCatalog : VersionEntity
{
    /// <summary>
    ///     子节点
    /// </summary>
    [Ignore]
    [JsonIgnore]
    [Navigate(nameof(ParentId))]
    public IEnumerable<Sys_DicCatalog> Children { get; init; }

    /// <summary>
    ///     字典编码
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    [Ignore]
    [JsonIgnore]
    public virtual string Code { get; init; }

    /// <summary>
    ///     字典内容集合
    /// </summary>
    [Ignore]
    [JsonIgnore]
    [Navigate(nameof(Sys_DicContent.CatalogId))]
    public ICollection<Sys_DicContent> Contents { get; init; }

    /// <summary>
    ///     字典名称
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    [Ignore]
    [JsonIgnore]
    public virtual string Name { get; init; }

    /// <summary>
    ///     父编号
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public virtual long ParentId { get; init; }
}