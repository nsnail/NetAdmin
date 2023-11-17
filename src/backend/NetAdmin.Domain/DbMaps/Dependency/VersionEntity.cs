using NetAdmin.Domain.Attributes;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.DbMaps.Dependency;

/// <inheritdoc />
public abstract record VersionEntity : VersionEntity<long>
{
    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [Snowflake]
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public override long Id { get; init; }
}

/// <summary>
///     乐观锁可变实体
/// </summary>
public abstract record VersionEntity<T> : LiteVersionEntity<T>, IFieldModifiedUser, IFieldCreatedUser
{
    /// <inheritdoc />
    [JsonIgnore]
    [Column(CanUpdate = false, Position = -1)]
    public long? CreatedUserId { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31, CanUpdate = false, Position = -1)]
    public string CreatedUserName { get; set; }

    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public override T Id { get; init; }

    /// <inheritdoc cref="IFieldModifiedUser.ModifiedUserId" />
    [JsonIgnore]
    [Column(CanInsert = false, Position = -1)]
    public long? ModifiedUserId { get; init; }

    /// <inheritdoc cref="IFieldModifiedUser.ModifiedUserName" />
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31, CanInsert = false, Position = -1)]
    public string ModifiedUserName { get; init; }
}