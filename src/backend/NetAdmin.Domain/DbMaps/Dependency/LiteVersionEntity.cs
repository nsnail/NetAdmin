namespace NetAdmin.Domain.DbMaps.Dependency;

/// <inheritdoc />
public abstract record LiteVersionEntity : LiteVersionEntity<long>
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [CsvIgnore]
    [Snowflake]
    public override long Id { get; init; }
}

/// <summary>
///     乐观锁轻型可变实体
/// </summary>
public abstract record LiteVersionEntity<T> : LiteMutableEntity<T>, IFieldVersion
    where T : IEquatable<T>
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [CsvIgnore]
    [Snowflake]
    public override T Id { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [Column(IsVersion = true, Position = -1)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long Version { get; init; }
}