using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.DbMaps;

/// <summary>
///     字典表
/// </summary>
[Table]
public record TbSysDic : MutableEntity, IFieldBitSet
{
    /// <summary>
    ///     比特位（前4位是公共位<see cref="EntityBase.BitSets" />）
    /// </summary>
    [JsonIgnore]
    public virtual long BitSet { get; init; }

    /// <summary>
    ///     子节点
    /// </summary>
    [Navigate(nameof(ParentId))]
    [JsonIgnore]
    public virtual IEnumerable<TbSysDic> Children { get; init; }

    /// <summary>
    ///     字典编码
    /// </summary>
    [JsonIgnore]
    public virtual string Code { get; init; }

    /// <summary>
    ///     字典名称
    /// </summary>
    [JsonIgnore]
    public virtual string Label { get; init; }

    /// <summary>
    ///     父id
    /// </summary>
    [JsonIgnore]
    public virtual long ParentId { get; init; }

    /// <summary>
    ///     字典值
    /// </summary>
    [JsonIgnore]
    public virtual string Value { get; init; }
}