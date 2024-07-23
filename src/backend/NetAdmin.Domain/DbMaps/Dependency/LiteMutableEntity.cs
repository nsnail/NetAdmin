namespace NetAdmin.Domain.DbMaps.Dependency;

/// <inheritdoc />
public abstract record LiteMutableEntity : LiteMutableEntity<long>
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [CsvIgnore]
    [Snowflake]
    public override long Id { get; init; }
}

/// <summary>
///     轻型可变实体
/// </summary>
public abstract record LiteMutableEntity<T> : LiteImmutableEntity<T>, IFieldModifiedTime
    where T : IEquatable<T>
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [CsvIgnore]
    public override T Id { get; init; }

    /// <inheritdoc cref="IFieldModifiedTime.ModifiedTime" />
    [Column(ServerTime = DateTimeKind.Local, CanInsert = false, Position = -1)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual DateTime? ModifiedTime { get; init; }
}