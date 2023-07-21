namespace NetAdmin.Domain.DbMaps.Dependency.Fields;

/// <summary>
///     启用字段接口
/// </summary>
public interface IFieldEnabled
{
    /// <summary>
    ///     是否启用
    /// </summary>
    bool Enabled { get; init; }
}