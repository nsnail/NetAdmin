namespace NetAdmin.Domain.DbMaps.Dependency.Fields;

/// <summary>
///     更新用户字段接口
/// </summary>
public interface IFieldModifiedUser
{
    /// <summary>
    ///     修改者Id
    /// </summary>
    long? ModifiedUserId { get; init; }

    /// <summary>
    ///     修改者
    /// </summary>
    string ModifiedUserName { get; init; }
}