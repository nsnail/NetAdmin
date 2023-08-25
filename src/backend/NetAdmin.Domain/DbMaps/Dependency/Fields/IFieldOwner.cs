namespace NetAdmin.Domain.DbMaps.Dependency.Fields;

/// <summary>
///     拥有者字段接口
/// </summary>
public interface IFieldOwner
{
    /// <summary>
    ///     拥有者部门编号
    /// </summary>
    long? OwnerDeptId { get; init; }

    /// <summary>
    ///     拥有者用户编号
    /// </summary>
    long? OwnerId { get; init; }
}