namespace NetAdmin.Domain.DbMaps.Dependency.Fields;

/// <summary>
///     版本字段接口
/// </summary>
public interface IFieldVersion
{
    /// <summary>
    ///     数据版本
    /// </summary>
    long Version { get; init; }
}