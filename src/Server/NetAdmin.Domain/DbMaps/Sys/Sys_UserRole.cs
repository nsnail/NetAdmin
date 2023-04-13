using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     用户-角色映射表
/// </summary>
[Table(Name = nameof(Sys_UserRole))]
public record Sys_UserRole : VersionEntity
{
    /// <summary>
    ///     关联的角色
    /// </summary>
    [JsonIgnore]
    public Sys_Role Role { get; init; }

    /// <summary>
    ///     角色id
    /// </summary>
    [JsonIgnore]
    public long RoleId { get; init; }

    /// <summary>
    ///     关联的用户
    /// </summary>
    [JsonIgnore]
    public Sys_User User { get; init; }

    /// <summary>
    ///     用户id
    /// </summary>
    [JsonIgnore]
    public long UserId { get; init; }
}