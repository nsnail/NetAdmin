namespace NetAdmin.Domain.DbMaps.Dependency.Fields;

/// <summary>
///     备注字段接口
/// </summary>
public interface IFieldSummary
{
    /// <summary>
    ///     备注
    /// </summary>
    string Summary { get; init; }
}