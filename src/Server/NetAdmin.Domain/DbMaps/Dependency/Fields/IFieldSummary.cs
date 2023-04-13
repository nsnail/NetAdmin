namespace NetAdmin.Domain.DbMaps.Dependency.Fields;

/// <summary>
///     描述字段接口
/// </summary>
public interface IFieldSummary
{
    /// <summary>
    ///     描述
    /// </summary>
    string Summary { get; init; }
}