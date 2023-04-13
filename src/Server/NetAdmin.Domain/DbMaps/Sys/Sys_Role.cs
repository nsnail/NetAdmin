using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     角色表
/// </summary>
[Table(Name = nameof(Sys_Role))]
[Index("idx_{tablename}_01", nameof(Name), true)]
public record Sys_Role : VersionEntity, IFieldSort, IFieldSummary
{
    /// <summary>
    ///     角色-接口映射
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_RoleApi))]
    public ICollection<Sys_Api> Apis { get; init; }

    /// <summary>
    ///     数据范围
    /// </summary>
    [JsonIgnore]
    public virtual DataScopes DataScope { get; init; }

    /// <summary>
    ///     角色-部门映射
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_RoleDept))]
    public ICollection<Sys_Dept> Depts { get; init; }

    /// <summary>
    ///     是否显示仪表板
    /// </summary>
    [JsonIgnore]
    public virtual bool DisplayDashboard { get; init; }

    /// <summary>
    ///     是否忽略权限控制
    /// </summary>
    [JsonIgnore]
    public virtual bool IgnorePermissionControl { get; init; }

    /// <summary>
    ///     角色-菜单映射
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_RoleMenu))]
    public ICollection<Sys_Menu> Menus { get; init; }

    /// <summary>
    ///     角色名
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR31)]
    public virtual string Name { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    public virtual long Sort { get; init; } = Numbers.DEF_SORT_VAL;

    /// <summary>
    ///     备注
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR255)]
    public virtual string Summary { get; init; }

    /// <summary>
    ///     此角色下的用户集合
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_UserRole))]
    public ICollection<Sys_User> Users { get; init; }
}