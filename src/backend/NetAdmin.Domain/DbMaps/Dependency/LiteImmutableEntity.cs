using NetAdmin.Domain.Attributes;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.DbMaps.Dependency;

/// <inheritdoc />
public abstract record LiteImmutableEntity : LiteImmutableEntity<long>
{
    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [Snowflake]
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public override long Id { get; init; }
}

/// <summary>
///     轻型不可变实体
/// </summary>
/// <typeparam name="T">主键类型</typeparam>
public abstract record LiteImmutableEntity<T> : EntityBase, IFieldPrimary<T>, IFieldCreatedTime
{
    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore]
    [Column(ServerTime = DateTimeKind.Utc, CanUpdate = false, Position = -1)]
    public virtual DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [JsonIgnore]
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public virtual T Id { get; init; }
}