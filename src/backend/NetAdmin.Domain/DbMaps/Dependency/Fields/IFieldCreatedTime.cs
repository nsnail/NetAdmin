namespace NetAdmin.Domain.DbMaps.Dependency.Fields;

/// <summary>
///     创建时间字段接口
/// </summary>
public interface IFieldCreatedTime
{
    /// <summary>
    ///     创建时间
    /// </summary>
    DateTime CreatedTime { get; init; }
}