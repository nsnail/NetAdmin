using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.DbMaps.Sys;

/// <summary>
///     字典内容表
/// </summary>
[Table]
[Index($"idx_{{tablename}}_{nameof(CatalogId)}_{nameof(Key)}",   $"{nameof(CatalogId)},{nameof(Key)}",   true)]
[Index($"idx_{{tablename}}_{nameof(CatalogId)}_{nameof(Value)}", $"{nameof(CatalogId)},{nameof(Value)}", true)]
public record TbSysDicContent : MutableEntity, IFieldBitSet
{
    /// <summary>
    ///     设置（前4位是公共位<see cref="EntityBase.BitSets" />）
    /// </summary>
    [JsonIgnore]
    public virtual long BitSet { get; init; }

    /// <summary>
    ///     字典目录
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(CatalogId))]
    public virtual TbSysDicCatalog Catalog { get; init; }

    /// <summary>
    ///     字典目录id
    /// </summary>
    [JsonIgnore]
    public virtual long CatalogId { get; init; }

    /// <summary>
    ///     项名称
    /// </summary>
    [JsonIgnore]
    public virtual string Key { get; init; }

    /// <summary>
    ///     键值
    /// </summary>
    [JsonIgnore]
    public virtual string Value { get; init; }
}