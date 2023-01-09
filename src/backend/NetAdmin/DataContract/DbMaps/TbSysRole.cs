using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.DbMaps;

/// <summary>
///     角色表
/// </summary>
[Table]
[Index("idx_{tablename}_01", nameof(Label), true)]
public record TbSysRole : DefaultEntity, IFieldBitSet, IFieldSort
{
    /// <summary>
    ///     比特位 <see cref="Enums.SysRoleBits" />
    /// </summary>
    [JsonIgnore]
    public virtual long BitSet { get; set; }

    /// <summary>
    ///     角色名
    /// </summary>
    [JsonIgnore]
    public virtual string Label { get; set; }

    /// <summary>
    ///     备注
    /// </summary>
    [JsonIgnore]
    public virtual string Remark { get; set; }

    /// <inheritdoc />
    [JsonIgnore]
    public virtual int Sort { get; set; }
}