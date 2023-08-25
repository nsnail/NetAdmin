using NetAdmin.Domain.Attributes;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.DbMaps.Dependency;

/// <summary>
///     轻型可变实体
/// </summary>
public abstract record LiteMutableEntity : LiteMutableEntity<long>
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    [Snowflake]
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public override long Id { get; init; }
}

/// <summary>
///     轻型可变实体
/// </summary>
public abstract record LiteMutableEntity<T> : LiteImmutableEntity<T>, IFieldModifiedTime
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public override T Id { get; init; }

    /// <summary>
    ///     修改时间
    /// </summary>
    [JsonIgnore]
    [Column(ServerTime = DateTimeKind.Utc, CanInsert = false, Position = -1)]
    public virtual DateTime? ModifiedTime { get; init; }
}