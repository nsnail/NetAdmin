namespace NetAdmin.Domain.DbMaps.Dependency;

/// <inheritdoc />
public abstract record ImmutableEntity : ImmutableEntity<long>
{
    /// <summary>
    ///     唯一编码
    /// </summary>
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
    /// <summary>
    ///     创建者编号
    /// </summary>
    [Column(CanUpdate = false, Position = -1)]
    [JsonIgnore]
    public virtual long? CreatedUserId { get; init; }

    /// <summary>
    ///     创建者用户名
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31, CanUpdate = false, Position = -1)]
    [JsonIgnore]
    public virtual string CreatedUserName { get; init; }

    /// <summary>
    ///     唯一编码
    /// </summary>
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public override T Id { get; init; }
}