namespace NetAdmin.Domain.DbMaps.Dependency.Fields;

/// <summary>
///     修改客户端字段接口
/// </summary>
public interface IFieldModifiedClient
{
    /// <summary>
    ///     客户端IP
    /// </summary>
    int ModifiedClientIp { get; init; }

    /// <summary>
    ///     客户端用户代理
    /// </summary>
    string ModifiedUserAgent { get; init; }
}