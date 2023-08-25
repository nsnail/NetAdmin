using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     角色-部门映射表
/// </summary>
[Table(Name = Chars.FLG_TABLE_NAME_PREFIX + nameof(Sys_RoleDept))]
[Index($"idx_{{tablename}}_{nameof(RoleId)}_{nameof(DeptId)}", $"{nameof(RoleId)},{nameof(DeptId)}", true)]
public sealed record Sys_RoleDept : ImmutableEntity
{
    /// <summary>
    ///     关联的部门
    /// </summary>
    [JsonIgnore]
    public Sys_Dept Dept { get; init; }

    /// <summary>
    ///     可访问的部门编号
    /// </summary>
    [JsonIgnore]
    [Column]
    public long DeptId { get; init; }

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
}