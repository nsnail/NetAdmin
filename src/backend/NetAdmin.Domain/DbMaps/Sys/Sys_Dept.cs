using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     部门表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_Dept))]
public record Sys_Dept : VersionEntity, IFieldEnabled, IFieldSummary, IFieldSort
{
    /// <summary>
    ///     子节点
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(ParentId))]
    public IEnumerable<Sys_Dept> Children { get; init; }

    /// <summary>
    ///     是否启用
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual bool Enabled { get; init; }

    /// <summary>
    ///     部门名称
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    [JsonIgnore]
    public virtual string Name { get; init; }

    /// <summary>
    ///     父编号
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual long ParentId { get; init; }

    /// <summary>
    ///     角色集合
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_RoleDept))]
    public ICollection<Sys_Role> Roles { get; init; }

    /// <summary>
    ///     发送给此部门的站内信集合
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_SiteMsgDept))]
    public ICollection<Sys_SiteMsg> SiteMsgs { get; init; }

    /// <summary>
    ///     排序值，越大越前
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual long Sort { get; init; }

    /// <summary>
    ///     部门描述
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [JsonIgnore]
    public virtual string Summary { get; init; }
}