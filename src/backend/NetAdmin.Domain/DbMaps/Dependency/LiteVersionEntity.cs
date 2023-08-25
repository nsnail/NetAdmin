using NetAdmin.Domain.Attributes;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.DbMaps.Dependency;

/// <summary>
///     乐观锁轻型可变实体
/// </summary>
public abstract record LiteVersionEntity : LiteVersionEntity<long>
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    [Snowflake]
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public override long Id { get; init; }
}

/// <summary>
///     乐观锁轻型可变实体
/// </summary>
public abstract record LiteVersionEntity<T> : LiteMutableEntity<T>, IFieldVersion
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public override T Id { get; init; }

    /// <summary>
    ///     数据版本
    /// </summary>
    [JsonIgnore]
    [Column(IsVersion = true, Position = -1)]
    public virtual long Version { get; init; }
}