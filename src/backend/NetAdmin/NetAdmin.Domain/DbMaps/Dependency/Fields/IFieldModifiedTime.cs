namespace NetAdmin.Domain.DbMaps.Dependency.Fields;

/// <summary>
///     修改时间字段接口
/// </summary>
public interface IFieldModifiedTime
{
    /// <summary>
    ///     修改时间
    /// </summary>
    DateTime? ModifiedTime { get; init; }
}