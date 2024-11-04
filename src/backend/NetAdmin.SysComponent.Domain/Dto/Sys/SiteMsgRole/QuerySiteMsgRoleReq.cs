namespace NetAdmin.SysComponent.Domain.Dto.Sys.SiteMsgRole;

/// <summary>
///     请求：查询站内信-角色映射
/// </summary>
public sealed record QuerySiteMsgRoleReq : Sys_SiteMsgRole
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}