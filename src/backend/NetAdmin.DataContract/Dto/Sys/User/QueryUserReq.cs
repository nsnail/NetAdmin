namespace NetAdmin.DataContract.Dto.Sys.User;

/// <summary>
///     请求：查询用户
/// </summary>
public record QueryUserReq : DataAbstraction
{
    /// <summary>
    ///     部门id
    /// </summary>
    public long DeptId { get; init; }

    /// <summary>
    ///     角色id
    /// </summary>
    public long RoleId { get; init; }
}