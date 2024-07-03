namespace NetAdmin.Domain.DbMaps.Dependency;

/// <inheritdoc />
public abstract record LiteImmutableEntity : LiteImmutableEntity<long>
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [Snowflake]
    public override long Id { get; init; }
}

/// <summary>
///     轻型不可变实体
/// </summary>
/// <typeparam name="T">主键类型</typeparam>
public abstract record LiteImmutableEntity<T> : EntityBase<T>, IFieldCreatedTime
    where T : IEquatable<T>
{
    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [Column(ServerTime = DateTimeKind.Local, CanUpdate = false, Position = -1)]
    [Ignore]
    [JsonIgnore]
    public virtual DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [Ignore]
    [JsonIgnore]
    public override T Id { get; init; }
}