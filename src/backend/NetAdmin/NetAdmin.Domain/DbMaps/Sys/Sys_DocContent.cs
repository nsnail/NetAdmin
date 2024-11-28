using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     文档内容表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_DocContent))]
public record Sys_DocContent : VersionEntity, IFieldEnabled, IFieldOwner
{
    /// <summary>
    ///     文档正文
    /// </summary>
    [Column(DbType = Chars.FLGL_DB_FIELD_TYPE_VARCHAR_MAX)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Body { get; init; }

    /// <summary>
    ///     文档分类
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(nameof(CatalogId))]
    public Sys_DocCatalog Catalog { get; init; }

    /// <summary>
    ///     文档分类编号
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long CatalogId { get; init; }

    /// <summary>
    ///     是否启用
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual bool Enabled { get; init; }

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
    ///     文档标题
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Title { get; init; }

    /// <summary>
    ///     可见性
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual ArchiveVisibilities Visibility { get; init; }
}