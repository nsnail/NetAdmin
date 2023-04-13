namespace NetAdmin.Domain.DbMaps.Dependency.Fields;

/// <summary>
///     创建用户字段接口
/// </summary>
public interface IFieldCreatedUser
{
    /// <summary>
    ///     创建者Id
    /// </summary>
    long? CreatedUserId { get; init; }

    /// <summary>
    ///     创建者
    /// </summary>
    string CreatedUserName { get; set; }
}