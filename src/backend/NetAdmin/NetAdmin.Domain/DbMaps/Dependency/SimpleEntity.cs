namespace NetAdmin.Domain.DbMaps.Dependency;

/// <inheritdoc />
public abstract record SimpleEntity : SimpleEntity<long>
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [Snowflake]
    public override long Id { get; init; }
}

/// <summary>
///     简单实体
/// </summary>
public abstract record SimpleEntity<T> : EntityBase<T>
    where T : IEquatable<T>;