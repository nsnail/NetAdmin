namespace NetAdmin.Domain.DbMaps.Dependency;

/// <inheritdoc />
public abstract record LiteVersionEntity : LiteVersionEntity<long>
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [Snowflake]
    public override long Id { get; init; }
}

/// <summary>
///     乐观锁轻型可变实体
/// </summary>
public abstract record LiteVersionEntity<T> : LiteMutableEntity<T>, IFieldVersion
    where T : IEquatable<T>
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [Snowflake]
    public override T Id { get; init; }

    /// <summary>
    ///     数据版本
    /// </summary>
    [Column(IsVersion = true, Position = -1)]
    [JsonIgnore]
    public virtual long Version { get; init; }
}