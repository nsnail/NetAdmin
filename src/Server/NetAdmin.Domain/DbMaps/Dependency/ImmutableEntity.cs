using NetAdmin.Domain.Attributes;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.DbMaps.Dependency;

/// <summary>
///     不可变实体
/// </summary>
public abstract record ImmutableEntity : ImmutableEntity<long>
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    [Snowflake]
    public override long Id { get; init; }
}

/// <summary>
///     不可变实体
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract record ImmutableEntity<T> : LiteImmutableEntity<T>, IFieldCreatedUser
{
    /// <inheritdoc />
    [JsonIgnore]
    [Column(Position = -22, CanUpdate = false)]
    public long? CreatedUserId { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Column(Position = -21, DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR31, CanUpdate = false)]
    public virtual string CreatedUserName { get; set; }
}