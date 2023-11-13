using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     站内信-角色映射表
/// </summary>
[Table(Name = Chars.FLG_TABLE_NAME_PREFIX + nameof(Sys_SiteMsgRole))]
public record Sys_SiteMsgRole : ImmutableEntity
{
    /// <summary>
    ///     关联的角色
    /// </summary>
    [JsonIgnore]
    public Sys_Role Role { get; init; }

    /// <summary>
    ///     角色编号
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual long RoleId { get; init; }

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