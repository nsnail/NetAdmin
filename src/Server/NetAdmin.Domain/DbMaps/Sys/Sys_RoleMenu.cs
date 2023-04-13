using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     角色-菜单映射表
/// </summary>
[Table(Name = nameof(Sys_RoleMenu))]
[Index($"idx_{{tablename}}_{nameof(RoleId)}_{nameof(MenuId)}", $"{nameof(RoleId)},{nameof(MenuId)}", true)]
public record Sys_RoleMenu : ImmutableEntity
{
    /// <summary>
    ///     关联的菜单
    /// </summary>
    [JsonIgnore]
    public Sys_Menu Menu { get; init; }

    /// <summary>
    ///     菜单id
    /// </summary>
    [JsonIgnore]
    public virtual long MenuId { get; init; }

    /// <summary>
    ///     关联的角色
    /// </summary>
    [JsonIgnore]
    public Sys_Role Role { get; init; }

    /// <summary>
    ///     角色id
    /// </summary>
    [JsonIgnore]
    public virtual long RoleId { get; init; }
}