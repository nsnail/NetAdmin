using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.DbMaps.Tpl;

/// <summary>
///     示例表
/// </summary>
[Table]
public record TbTplExample : MutableEntity, IFieldBitSet
{
    /// <summary>
    ///     比特位（前4位是公共位<see cref="EntityBase.BitSets" />）
    /// </summary>
    public long BitSet { get; init; }
}