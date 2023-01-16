using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.DbMaps.Sys;

/// <summary>
///     角色-部门映射表
/// </summary>
[Table]
[Index($"idx_{{tablename}}_{nameof(RoleId)}_{nameof(DeptId)}", $"{nameof(RoleId)},{nameof(DeptId)}", true)]
public record TbSysRoleDept : ImmutableEntity
{
    /// <summary>
    ///     关联的部门
    /// </summary>
    [JsonIgnore]
    public TbSysDept Dept { get; init; }

    /// <summary>
    ///     可访问的部门id
    /// </summary>
    [JsonIgnore]
    public long DeptId { get; init; }

    /// <summary>
    ///     关联的角色
    /// </summary>
    [JsonIgnore]
    public TbSysRole Role { get; init; }

    /// <summary>
    ///     角色id
    /// </summary>
    [JsonIgnore]
    public long RoleId { get; init; }
}