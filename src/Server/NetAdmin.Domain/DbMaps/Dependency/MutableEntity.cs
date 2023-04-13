using NetAdmin.Domain.Attributes;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.DbMaps.Dependency;

/// <summary>
///     可变实体
/// </summary>
public abstract record MutableEntity : MutableEntity<long>
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    [Snowflake]
    public override long Id { get; init; }
}

/// <summary>
///     可变实体
/// </summary>
public abstract record MutableEntity<T> : LiteMutableEntity<T>, IFieldModifiedUser
{
    /// <inheritdoc />
    [JsonIgnore]
    [Column(Position = -13, CanInsert = false)]
    public long? ModifiedUserId { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Column(Position = -12, DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR31, CanInsert = false)]
    public string ModifiedUserName { get; init; }
}