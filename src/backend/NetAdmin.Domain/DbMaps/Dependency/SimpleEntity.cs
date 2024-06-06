namespace NetAdmin.Domain.DbMaps.Dependency;

/// <summary>
///     简单实体
/// </summary>
public abstract record SimpleEntity<T> : EntityBase<T>
    where T : IEquatable<T>;