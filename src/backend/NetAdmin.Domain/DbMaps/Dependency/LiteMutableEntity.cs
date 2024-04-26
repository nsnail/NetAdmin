using NetAdmin.Domain.Attributes;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.DbMaps.Dependency;

/// <inheritdoc />
public abstract record LiteMutableEntity : LiteMutableEntity<long>
{
    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [Snowflake]
    public override long Id { get; init; }
}

/// <summary>
///     轻型可变实体
/// </summary>
public abstract record LiteMutableEntity<T> : LiteImmutableEntity<T>, IFieldModifiedTime
{
    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public override T Id { get; init; }

    /// <inheritdoc cref="IFieldModifiedTime.ModifiedTime" />
    [Column(ServerTime = DateTimeKind.Local, CanInsert = false, Position = -1)]
    [JsonIgnore]
    public virtual DateTime? ModifiedTime { get; init; }
}