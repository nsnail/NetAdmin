namespace NetAdmin.Domain.DbMaps.Dependency;

/// <summary>
///     排序字段接口
/// </summary>
public interface IFieldSort
{
    /// <summary>
    ///     排序值，越大越前
    /// </summary>
    long Sort { get; init; }
}