using NetAdmin.Domain.Attributes;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.DbMaps.Dependency;

/// <summary>
///     乐观锁可变实体
/// </summary>
public abstract record VersionEntity : VersionEntity<long>
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    [Snowflake]
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public override long Id { get; init; }
}

/// <summary>
///     乐观锁可变实体
/// </summary>
public abstract record VersionEntity<T> : LiteVersionEntity<T>, IFieldModifiedUser
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public override T Id { get; init; }

    /// <summary>
    ///     修改者编号
    /// </summary>
    [JsonIgnore]
    [Column(CanInsert = false, Position = -1)]
    public long? ModifiedUserId { get; init; }

    /// <summary>
    ///     修改者用户名
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31, CanInsert = false, Position = -1)]
    public string ModifiedUserName { get; init; }
}