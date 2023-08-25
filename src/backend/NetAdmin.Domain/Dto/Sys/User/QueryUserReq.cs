using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：查询用户
/// </summary>
public sealed record QueryUserReq : Sys_User
{
    /// <summary>
    ///     部门编号
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long DeptId { get; init; }

    /// <summary>
    ///     id
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <summary>
    ///     角色编号
    /// </summary>
    public long RoleId { get; init; }
}