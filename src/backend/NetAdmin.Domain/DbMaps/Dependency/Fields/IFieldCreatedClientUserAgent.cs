namespace NetAdmin.Domain.DbMaps.Dependency.Fields;

/// <summary>
///     创建者客户端用户代理字段接口
/// </summary>
public interface IFieldCreatedClientUserAgent
{
    /// <summary>
    ///     创建者客户端用户代理
    /// </summary>
    string CreatedUserAgent { get; init; }
}