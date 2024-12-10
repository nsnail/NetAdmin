using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.UserRole;

/// <summary>
///     响应：查询用户-角色映射
/// </summary>
public sealed record QueryUserRoleRsp : Sys_UserRole
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}