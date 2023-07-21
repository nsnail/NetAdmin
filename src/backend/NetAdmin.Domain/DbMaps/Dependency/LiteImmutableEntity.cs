using NetAdmin.Domain.Attributes;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.DbMaps.Dependency;

/// <summary>
///     轻型不可变实体
/// </summary>
public abstract record LiteImmutableEntity : LiteImmutableEntity<long>
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    [Snowflake]
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public override long Id { get; init; }
}

/// <summary>
///     轻型不可变实体
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract record LiteImmutableEntity<T> : EntityBase, IFieldPrimary<T>, IFieldCreatedTime
{
    /// <summary>
    ///     创建时间
    /// </summary>
    [JsonIgnore]
    [Column(ServerTime = DateTimeKind.Utc, CanUpdate = false, Position = -1)]
    public virtual DateTime CreatedTime { get; init; }

    /// <summary>
    ///     唯一编码
    /// </summary>
    [JsonIgnore]
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public virtual T Id { get; init; }
}