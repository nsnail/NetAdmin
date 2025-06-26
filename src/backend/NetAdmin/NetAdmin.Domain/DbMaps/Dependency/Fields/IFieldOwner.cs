namespace NetAdmin.Domain.DbMaps.Dependency.Fields;

/// <summary>
///     所有者字段接口
/// </summary>
public interface IFieldOwner
{
    /// <summary>
    ///     所有者部门编号
    /// </summary>
    long? OwnerDeptId { get; init; }

    /// <summary>
    ///     所有者用户编号
    /// </summary>
    long? OwnerId { get; init; }
}