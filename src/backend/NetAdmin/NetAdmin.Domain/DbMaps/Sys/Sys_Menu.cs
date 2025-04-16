using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     菜单表
/// </summary>
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(Name), nameof(Name), true)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_Menu))]
public record Sys_Menu : VersionEntity, IFieldSort
{
    /// <summary>
    ///     子节点或详情页需要高亮的上级菜单路由地址
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Active { get; init; }

    /// <summary>
    ///     子节点
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(nameof(ParentId))]
    public IEnumerable<Sys_Menu> Children { get; init; }

    /// <summary>
    ///     背景颜色
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_7)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Color { get; init; }

    /// <summary>
    ///     组件
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Component { get; init; }

    /// <summary>
    ///     是否整页路由
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual bool FullPageRouting { get; init; }

    /// <summary>
    ///     是否隐藏菜单
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual bool Hidden { get; init; }

    /// <summary>
    ///     是否隐藏面包屑
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual bool HiddenBreadCrumb { get; init; }

    /// <summary>
    ///     图标
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Icon { get; init; }

    /// <summary>
    ///     菜单名称
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Name { get; init; }

    /// <summary>
    ///     父编号
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long ParentId { get; init; }

    /// <summary>
    ///     菜单路径
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Path { get; init; }

    /// <summary>
    ///     重定向地址
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Redirect { get; init; }

    /// <summary>
    ///     拥有此菜单的角色集合
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_RoleMenu))]
    public IReadOnlyCollection<Sys_Role> Roles { get; init; }

    /// <summary>
    ///     排序值，越大越前
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long Sort { get; init; }

    /// <summary>
    ///     标签
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Tag { get; init; }

    /// <summary>
    ///     菜单标题
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Title { get; init; }

    /// <summary>
    ///     菜单类型
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual MenuTypes Type { get; init; }
}