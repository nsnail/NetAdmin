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
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public override long Id { get; init; }
}

/// <summary>
///     不可变实体
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract record ImmutableEntity<T> : LiteImmutableEntity<T>, IFieldCreatedUser
{
    /// <summary>
    ///     创建者编号
    /// </summary>
    [JsonIgnore]
    [Column(CanUpdate = false, Position = -1)]
    public long? CreatedUserId { get; init; }

    /// <summary>
    ///     创建者用户名
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31, CanUpdate = false, Position = -1)]
    public virtual string CreatedUserName { get; set; }

    /// <summary>
    ///     唯一编码
    /// </summary>
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public override T Id { get; init; }
}