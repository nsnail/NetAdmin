namespace NetAdmin.Domain.DbMaps.Dependency;

/// <summary>
///     数据库实体基类
/// </summary>
public abstract record EntityBase<T> : DataAbstraction
    where T : IEquatable<T>
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    public virtual T Id { get; init; }
}