using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     部门表
/// </summary>
[Table]
public record TbSysDept : MutableEntity, IFieldBitSet, IFieldSummary
{
    /// <summary>
    ///     设置（前4位是公共位<see cref="EntityBase.BitSets" />）
    /// </summary>
    [JsonIgnore]
    public virtual long BitSet { get; init; }

    /// <summary>
    ///     子节点
    /// </summary>
    [Navigate(nameof(ParentId))]
    [JsonIgnore]
    public virtual IEnumerable<TbSysDept> Children { get; init; }

    /// <summary>
    ///     部门名称
    /// </summary>
    [JsonIgnore]
    [MaxLength(32)]
    public virtual string Name { get; init; }

    /// <summary>
    ///     父id
    /// </summary>
    [JsonIgnore]
    public virtual long ParentId { get; init; }

    /// <summary>
    ///     角色集合
    /// </summary>
    [Navigate(ManyToMany = typeof(TbSysRoleDept))]
    public virtual IReadOnlyCollection<TbSysRole> Roles { get; init; }

    /// <summary>
    ///     排序
    /// </summary>
    [JsonIgnore]
    public virtual int Sort { get; init; }

    /// <summary>
    ///     部门描述
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    public virtual string Summary { get; init; }
}