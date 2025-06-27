namespace NetAdmin.Domain.DbMaps.Dependency.Fields;

/// <summary>
///     归属字段接口
/// </summary>
public interface IFieldOwner
{
    /// <summary>
    ///     归属部门编号
    /// </summary>
    long? OwnerDeptId { get; init; }

    /// <summary>
    ///     归属用户编号
    /// </summary>
    long? OwnerId { get; init; }
}