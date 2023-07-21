using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     用户-角色映射表
/// </summary>
[Table(Name = Chars.FLG_TABLE_NAME_PREFIX + nameof(Sys_UserRole))]
public sealed record Sys_UserRole : VersionEntity
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
    public long RoleId { get; init; }

    /// <summary>
    ///     关联的用户
    /// </summary>
    [JsonIgnore]
    public Sys_User User { get; init; }

    /// <summary>
    ///     用户编号
    /// </summary>
    [JsonIgnore]
    [Column]
    public long UserId { get; init; }
}