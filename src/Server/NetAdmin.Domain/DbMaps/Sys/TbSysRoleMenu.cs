using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     角色-菜单映射表
/// </summary>
[Table]
[Index($"idx_{{tablename}}_{nameof(RoleId)}_{nameof(MenuId)}", $"{nameof(RoleId)},{nameof(MenuId)}", true)]
public record TbSysRoleMenu : ImmutableEntity
{
    /// <summary>
    ///     关联的菜单
    /// </summary>
    [JsonIgnore]
    public virtual TbSysMenu Menu { get; init; }

    /// <summary>
    ///     菜单id
    /// </summary>
    [JsonIgnore]
    public virtual long MenuId { get; init; }

    /// <summary>
    ///     关联的角色
    /// </summary>
    [JsonIgnore]
    public virtual TbSysRole Role { get; init; }

    /// <summary>
    ///     角色id
    /// </summary>
    [JsonIgnore]
    public virtual long RoleId { get; init; }
}