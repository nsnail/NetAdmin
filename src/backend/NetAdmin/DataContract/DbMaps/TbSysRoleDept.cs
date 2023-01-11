using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.DbMaps;

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
    public TbSysDept Dept { get; set; }

    /// <summary>
    ///     可访问的部门id
    /// </summary>
    [JsonIgnore]
    public long DeptId { get; set; }

    /// <summary>
    ///     关联的角色
    /// </summary>
    [JsonIgnore]
    public TbSysRole Role { get; set; }

    /// <summary>
    ///     角色id
    /// </summary>
    [JsonIgnore]
    public long RoleId { get; set; }
}