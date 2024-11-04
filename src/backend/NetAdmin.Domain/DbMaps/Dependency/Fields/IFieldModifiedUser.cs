namespace NetAdmin.Domain.DbMaps.Dependency.Fields;

/// <summary>
///     修改用户字段接口
/// </summary>
public interface IFieldModifiedUser
{
    /// <summary>
    ///     修改者编号
    /// </summary>
    long? ModifiedUserId { get; init; }

    /// <summary>
    ///     修改者用户名
    /// </summary>
    string ModifiedUserName { get; init; }
}