namespace NetAdmin.Domain.DbMaps.Dependency.Fields;

/// <summary>
///     创建者客户端IP字段接口
/// </summary>
public interface IFieldCreatedClientIp
{
    /// <summary>
    ///     创建者客户端IP
    /// </summary>
    int? CreatedClientIp { get; init; }
}