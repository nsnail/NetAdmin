namespace NetAdmin.Domain.DbMaps.Dependency.Fields;

/// <summary>
///     修改客户端用户代理字段接口
/// </summary>
public interface IFieldModifiedClientUserAgent
{
    /// <summary>
    ///     客户端用户代理
    /// </summary>
    string ModifiedUserAgent { get; init; }
}