using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     岗位表
/// </summary>
[Table]
[Index($"idx_{{tablename}}_{nameof(Name)}", nameof(Name), true)]
public record TbSysPosition : MutableEntity, IFieldBitSet, IFieldSort, IFieldSummary
{
    /// <summary>
    ///     设置（前4位是公共位<see cref="EntityBase.BitSets" />）
    /// </summary>
    [JsonIgnore]
    public virtual long BitSet { get; init; }

    /// <summary>
    ///     岗位名称
    /// </summary>
    [JsonIgnore]
    [MaxLength(31)]
    public virtual string Name { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    public virtual long Sort { get; init; }

    /// <summary>
    ///     岗位描述
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    public virtual string Summary { get; init; }

    /// <summary>
    ///     此岗位下的用户集合
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(TbSysUserPosition))]
    public ICollection<TbSysUser> Users { get; init; }
}