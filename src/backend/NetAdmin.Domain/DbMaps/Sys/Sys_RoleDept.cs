using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     角色-部门映射表
/// </summary>
[Index($"idx_{{tablename}}_{nameof(RoleId)}_{nameof(DeptId)}", $"{nameof(RoleId)},{nameof(DeptId)}", true)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_RoleDept))]
public record Sys_RoleDept : ImmutableEntity
{
    /// <summary>
    ///     关联的部门
    /// </summary>
    [JsonIgnore]
    public Sys_Dept Dept { get; init; }

    /// <summary>
    ///     可访问的部门编号
    /// </summary>
    [Column]
    [JsonIgnore]
    public long DeptId { get; init; }

    /// <summary>
    ///     关联的角色
    /// </summary>
    [JsonIgnore]
    public Sys_Role Role { get; init; }

    /// <summary>
    ///     角色编号
    /// </summary>
    [Column]
    [JsonIgnore]
    public long RoleId { get; init; }
}