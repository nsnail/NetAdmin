using FreeSql.DataAnnotations;
using {0}.DataContract.DbMaps.Dependency;

namespace {0}.DataContract.DbMaps;

/// <summary>
///     {3}表
/// </summary>
[Table]
public record Tb{1}{2} : MutableEntity, IFieldBitSet
{{
    /// <summary>
    ///     设置（前4位是公共位<see cref="EntityBase.BitSets" />）
    /// </summary>
    [JsonIgnore]
    public virtual long BitSet {{ get; init; }}
}}