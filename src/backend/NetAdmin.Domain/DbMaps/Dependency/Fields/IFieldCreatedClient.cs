namespace NetAdmin.Domain.DbMaps.Dependency.Fields;

/// <summary>
///     创建者客户端字段接口
/// </summary>
public interface IFieldCreatedClient
{
    /// <summary>
    ///     创建者客户端IP
    /// </summary>
    int? CreatedClientIp { get; init; }

    /// <summary>
    ///     创建者来源地址
    /// </summary>
    string CreatedReferer { get; init; }

    /// <summary>
    ///     创建者客户端用户代理
    /// </summary>
    string CreatedUserAgent { get; init; }
}