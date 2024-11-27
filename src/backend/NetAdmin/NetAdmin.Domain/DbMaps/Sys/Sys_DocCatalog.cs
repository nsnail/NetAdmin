namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     文档分类表
/// </summary>
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(Code), nameof(Code), true)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_DocCatalog))]
public record Sys_DocCatalog : VersionEntity, IFieldOwner
{
    /// <summary>
    ///     子节点
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(nameof(ParentId))]
    public IEnumerable<Sys_DocCatalog> Children { get; init; }

    /// <summary>
    ///     文档分类编码
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Code { get; init; }

    /// <summary>
    ///     文档内容集合
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(nameof(Sys_DocContent.CatalogId))]
    public ICollection<Sys_DocContent> Contents { get; init; }

    /// <summary>
    ///     文档分类名称
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Name { get; init; }

    /// <summary>
    ///     拥有者
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(nameof(OwnerId))]
    public Sys_User Owner { get; init; }

    /// <summary>
    ///     拥有者部门编号
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long? OwnerDeptId { get; init; }

    /// <summary>
    ///     拥有者用户编号
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long? OwnerId { get; init; }

    /// <summary>
    ///     父编号
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long ParentId { get; init; }
}