using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.DbMaps;

/// <summary>
///     部门表
/// </summary>
[Table]
public record TbSysDept : DefaultEntity, IFieldBitSet
{
    /// <summary>
    ///     比特位 <see cref="Enums.SysDeptBits" />
    /// </summary>
    [JsonIgnore]
    public virtual long BitSet { get; set; }

    /// <summary>
    ///     子节点
    /// </summary>
    [Navigate(nameof(ParentId))]
    [JsonIgnore]
    public virtual List<TbSysDept> Children { get; set; }

    /// <summary>
    ///     部门名称
    /// </summary>
    [JsonIgnore]
    public virtual string Label { get; set; }

    /// <summary>
    ///     父id
    /// </summary>
    [JsonIgnore]
    public virtual long ParentId { get; set; }

    /// <summary>
    ///     部门描述
    /// </summary>
    [JsonIgnore]
    public virtual string Remark { get; set; }

    /// <summary>
    ///     角色集合
    /// </summary>
    [Navigate(ManyToMany = typeof(TbSysRoleDept))]
    public virtual ICollection<TbSysRole> Roles { get; set; }

    /// <summary>
    ///     排序
    /// </summary>
    [JsonIgnore]
    public virtual int Sort { get; set; }
}