using NetAdmin.Domain.Attributes;

namespace NetAdmin.Domain.DbMaps.Dependency;

/// <inheritdoc />
public abstract record SimpleEntity : SimpleEntity<long>
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [Snowflake]
    public override long Id { get; init; }
}

/// <summary>
///     简单实体
/// </summary>
public abstract record SimpleEntity<T> : EntityBase<T>
    where T : IEquatable<T>;