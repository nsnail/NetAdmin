using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.SiteMsgRole;

/// <summary>
///     响应：查询站内信-角色映射
/// </summary>
public sealed record QuerySiteMsgRoleRsp : Sys_SiteMsgRole
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}