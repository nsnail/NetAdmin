using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     站内信-部门映射表
/// </summary>
[Table(Name = Chars.FLG_TABLE_NAME_PREFIX + nameof(Sys_SiteMsgDept))]
public record Sys_SiteMsgDept : ImmutableEntity
{
    /// <summary>
    ///     关联的部门
    /// </summary>
    [JsonIgnore]
    public Sys_Dept Dept { get; init; }

    /// <summary>
    ///     部门编号
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual long DeptId { get; init; }

    /// <summary>
    ///     关联的站内信
    /// </summary>
    [JsonIgnore]
    public Sys_SiteMsg SiteMsg { get; init; }

    /// <summary>
    ///     站内信编号
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual long SiteMsgId { get; init; }
}