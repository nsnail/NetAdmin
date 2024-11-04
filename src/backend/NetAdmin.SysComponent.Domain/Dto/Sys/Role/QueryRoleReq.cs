namespace NetAdmin.SysComponent.Domain.Dto.Sys.Role;

/// <summary>
///     请求：查询角色
/// </summary>
public sealed record QueryRoleReq : Sys_Role
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}