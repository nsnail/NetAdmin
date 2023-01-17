using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps.Sys;

namespace NetAdmin.DataContract.Dto.Sys.User;

/// <summary>
///     请求：查询用户
/// </summary>
public record QueryUserReq : TbSysUser
{
    /// <summary>
    ///     部门id
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long DeptId { get; init; }

    /// <summary>
    ///     id
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }

    /// <summary>
    ///     角色id
    /// </summary>
    public long RoleId { get; init; }
}