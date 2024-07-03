namespace NetAdmin.Domain.DbMaps.Dependency;

/// <inheritdoc />
public abstract record ImmutableEntity : ImmutableEntity<long>
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [Snowflake]
    public override long Id { get; init; }
}

/// <summary>
///     不可变实体
/// </summary>
/// <typeparam name="T">主键类型</typeparam>
public abstract record ImmutableEntity<T> : LiteImmutableEntity<T>, IFieldCreatedUser
    where T : IEquatable<T>
{
    /// <inheritdoc cref="IFieldCreatedUser.CreatedUserId" />
    [Column(CanUpdate = false, Position = -1)]
    [Ignore]
    [JsonIgnore]
    public long? CreatedUserId { get; init; }

    /// <inheritdoc cref="IFieldCreatedUser.CreatedUserName" />
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31, CanUpdate = false, Position = -1)]
    [Ignore]
    [JsonIgnore]
    public virtual string CreatedUserName { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public override T Id { get; init; }
}