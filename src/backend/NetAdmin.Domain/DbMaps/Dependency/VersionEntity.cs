namespace NetAdmin.Domain.DbMaps.Dependency;

/// <inheritdoc />
public abstract record VersionEntity : VersionEntity<long>
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [CsvIgnore]
    [Snowflake]
    public override long Id { get; init; }
}

/// <summary>
///     乐观锁可变实体
/// </summary>
public abstract record VersionEntity<T> : LiteVersionEntity<T>, IFieldModifiedUser, IFieldCreatedUser
    where T : IEquatable<T>
{
    /// <summary>
    ///     创建者编号
    /// </summary>
    [Column(CanUpdate = false, Position = -1)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long? CreatedUserId { get; init; }

    /// <summary>
    ///     创建者用户名
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31, CanUpdate = false, Position = -1)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string CreatedUserName { get; init; }

    /// <summary>
    ///     唯一编码
    /// </summary>
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [CsvIgnore]
    public override T Id { get; init; }

    /// <summary>
    ///     修改者编号
    /// </summary>
    [Column(CanInsert = false, Position = -1)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long? ModifiedUserId { get; init; }

    /// <summary>
    ///     修改者用户名
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31, CanInsert = false, Position = -1)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string ModifiedUserName { get; init; }
}