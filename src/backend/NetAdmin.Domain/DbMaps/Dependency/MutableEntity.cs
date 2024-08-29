namespace NetAdmin.Domain.DbMaps.Dependency;

/// <inheritdoc />
public abstract record MutableEntity : MutableEntity<long>
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [CsvIgnore]
    [Snowflake]
    public override long Id { get; init; }
}

/// <summary>
///     可变实体
/// </summary>
public abstract record MutableEntity<T> : LiteMutableEntity<T>, IFieldCreatedUser, IFieldModifiedUser
    where T : IEquatable<T>
{
    /// <inheritdoc />
    [Column(CanUpdate = false, Position = -1)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long? CreatedUserId { get; init; }

    /// <inheritdoc />
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31, CanUpdate = false, Position = -1)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string CreatedUserName { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [CsvIgnore]
    public override T Id { get; init; }

    /// <inheritdoc cref="IFieldModifiedUser.ModifiedUserId" />
    [Column(CanInsert = false, Position = -1)]
    [CsvIgnore]
    [JsonIgnore]
    public long? ModifiedUserId { get; init; }

    /// <inheritdoc cref="IFieldModifiedUser.ModifiedUserName" />
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31, CanInsert = false, Position = -1)]
    [CsvIgnore]
    [JsonIgnore]
    public string ModifiedUserName { get; init; }
}