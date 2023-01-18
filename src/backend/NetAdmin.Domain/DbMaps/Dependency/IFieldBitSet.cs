namespace NetAdmin.Domain.DbMaps.Dependency;

/// <summary>
///     设置字段接口
/// </summary>
public interface IFieldBitSet
{
    /// <summary>
    ///     设置
    /// </summary>
    long BitSet { get; init; }
}