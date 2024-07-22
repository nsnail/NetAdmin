namespace NetAdmin.Domain.DbMaps.Dependency.Fields;

/// <summary>
///     修改客户端IP字段接口
/// </summary>
public interface IFieldModifiedClientIp
{
    /// <summary>
    ///     客户端IP
    /// </summary>
    int ModifiedClientIp { get; init; }
}