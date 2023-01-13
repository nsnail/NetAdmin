using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.DbMaps;

/// <summary>
///     部门表
/// </summary>
[Table]
public record TbSysDept : MutableEntity, IFieldBitSet
{
    /// <summary>
    ///     比特位（前4位是公共位<see cref="Enums.BitSets" />） <see cref="Enums.SysDeptBits" />
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
    public virtual string Label { get; init; }

    /// <summary>
    ///     父id
    /// </summary>
    [JsonIgnore]
    public virtual long ParentId { get; init; }

    /// <summary>
    ///     部门描述
    /// </summary>
    [JsonIgnore]
    public virtual string Remark { get; init; }

    /// <summary>
    ///     角色集合
    /// </summary>
    [Navigate(ManyToMany = typeof(TbSysRoleDept))]
    public virtual ICollection<TbSysRole> Roles { get; init; }

    /// <summary>
    ///     排序
    /// </summary>
    [JsonIgnore]
    public virtual int Sort { get; init; }
}