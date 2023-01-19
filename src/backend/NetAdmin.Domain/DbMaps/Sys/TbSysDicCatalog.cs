using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     字典目录表
/// </summary>
[Table]
[Index($"idx_{{tablename}}_{nameof(Code)}", nameof(Code), true)]
public record TbSysDicCatalog : MutableEntity, IFieldBitSet
{
    /// <summary>
    ///     设置（前4位是公共位<see cref="EntityBase.BitSets" />）
    /// </summary>
    [JsonIgnore]
    public virtual long BitSet { get; init; }

    /// <summary>
    ///     子节点
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(ParentId))]
    public virtual IEnumerable<TbSysDicCatalog> Children { get; init; }

    /// <summary>
    ///     字典编码
    /// </summary>
    [JsonIgnore]
    [MaxLength(31)]
    public virtual string Code { get; init; }

    /// <summary>
    ///     字典内容集合
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(TbSysDicContent.CatalogId))]
    public virtual ICollection<TbSysDicContent> Contents { get; init; }

    /// <summary>
    ///     字典名称
    /// </summary>
    [JsonIgnore]
    [MaxLength(31)]
    public virtual string Name { get; init; }

    /// <summary>
    ///     父id
    /// </summary>
    [JsonIgnore]
    public virtual long ParentId { get; init; }
}