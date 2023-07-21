using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     菜单表
/// </summary>
[Table(Name = "Sys_Menu")]
[Index($"idx_{{tablename}}_{nameof(Name)}", nameof(Name), true)]
public record Sys_Menu : VersionEntity, IFieldSort
{
    /// <summary>
    ///     子节点或详情页需要高亮的上级菜单路由地址
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    public virtual string Active { get; init; }

    /// <summary>
    ///     子节点
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(ParentId))]
    public IEnumerable<Sys_Menu> Children { get; init; }

    /// <summary>
    ///     背景颜色
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_7)]
    public virtual string Color { get; init; }

    /// <summary>
    ///     组件
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    public virtual string Component { get; init; }

    /// <summary>
    ///     是否整页路由
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual bool FullPageRouting { get; init; }

    /// <summary>
    ///     是否隐藏菜单
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual bool Hidden { get; init; }

    /// <summary>
    ///     是否隐藏面包屑
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual bool HiddenBreadCrumb { get; init; }

    /// <summary>
    ///     图标
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    public virtual string Icon { get; init; }

    /// <summary>
    ///     菜单名称
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    public virtual string Name { get; init; }

    /// <summary>
    ///     父编号
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual long ParentId { get; init; }

    /// <summary>
    ///     菜单路径
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    public virtual string Path { get; init; }

    /// <summary>
    ///     重定向地址
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    public virtual string Redirect { get; init; }

    /// <summary>
    ///     拥有此菜单的角色集合
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_RoleMenu))]
    public ICollection<Sys_Role> Roles { get; init; }

    /// <summary>
    ///     排序值，越大越前
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual long Sort { get; init; }

    /// <summary>
    ///     标签
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    public virtual string Tag { get; init; }

    /// <summary>
    ///     菜单标题
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    public virtual string Title { get; init; }

    /// <summary>
    ///     菜单类型
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual MenuTypes Type { get; init; }
}